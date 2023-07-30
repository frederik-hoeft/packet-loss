using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Linq;
using CombatAI;

namespace PostOffice.Patching.HarmonyPatches;

using static PostOfficeMod;

[HarmonyPatch(typeof(MusicManagerPlay), "DangerMusicMode", MethodType.Getter)]
[RequiresMod("Krkr.rule56")]
public static class MusicManagerPlay_DangerMusicModePatch
{
    public static bool Prefix(ref bool __result)
    {
        // override base game implementation only iff mod is enabled
        // and dependencies are loaded.
        if (Settings is { isActive: true, cai5000_delayCombatMusic: true})
        {
            // for every map check what the game thinks about the danger rating
            List<Map> maps = Find.Maps;
            for (int index = 0; index < maps.Count; ++index)
            {
                if (maps[index].dangerWatcher.DangerRating == StoryDanger.High)
                {
                    // do some additional checks and only play combat music iff
                    // enemies are *actually visible*
                    Map map = maps[index];
                    MapComponent_FogGrid? fog = map.GetComp_Fast<MapComponent_FogGrid>();
                    if (fog is not null)
                    {
                        // TODO: this could probably benefit from caching :P
                        __result = map.mapPawns.AllPawnsSpawned.Any(p => p.HostileTo(Faction.OfPlayerSilentFail) && !fog.IsFogged(p.Position));
                        Logger.LogVerbose($"DangerRating is high. RimWorld is attempting to play combat music! allowing combat music? {__result}");
                    }
                    else
                    {
                        // if fog of war is disabled or otherwise unavailable, allow default behavior
                        __result = true;
                    }
                    return false;
                }
            }
            __result = false;
            return false;
        }
        // forward to base game implementation
        return true;
    }
}
