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
        if (Settings?.isActive is true && Settings.cai5000_delayCombatMusic)
        {
            List<Map> maps = Find.Maps;
            for (int index = 0; index < maps.Count; ++index)
            {
                if (maps[index].dangerWatcher.DangerRating == StoryDanger.High)
                {
                    Map map = maps[index];
                    MapComponent_FogGrid? fog = map.GetComp_Fast<MapComponent_FogGrid>();
                    if (fog is not null)
                    {
                        // TODO: maybe cache hostile pawns?
                        __result = map.mapPawns.AllPawnsSpawned.Any(p => p.HostileTo(Faction.OfPlayerSilentFail) && !fog.IsFogged(p.Position));
                        Logger.LogVerbose($"DangerRating is high. RimWorld is attempting to play combat music! allowing combat music? {__result}");
                    }
                    else
                    {
                        __result = true;
                    }
                    return false;
                }
            }
            __result = false;
            return false;
        }
        return true;
    }
}
