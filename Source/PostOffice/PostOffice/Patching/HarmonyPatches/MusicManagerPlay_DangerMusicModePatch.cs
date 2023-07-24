using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Linq;
using CombatAI;
using Verse.Noise;

namespace PostOffice.Patching.HarmonyPatches;

using static PostOfficeMod;

[HarmonyPatch(typeof(MusicManagerPlay), "DangerMusicMode", MethodType.Getter)]
[RequiresMod("Krkr.rule56")]
public static class MusicManagerPlay_DangerMusicModePatch
{
    public static bool Prefix(ref bool __result)
    {
        if (Settings?.isActive is true)
        {
            Logger.LogVerbose($"intercepted {nameof(MusicManagerPlay)}.DangerMusicMode_get() using {nameof(MusicManagerPlay_DangerMusicModePatch)}.");
            List<Map> maps = Find.Maps;
            for (int index = 0; index < maps.Count; ++index)
            {
                if (maps[index].dangerWatcher.DangerRating == StoryDanger.High)
                {
                    Logger.LogVerbose($"DangerRating is high. RimWorld is attempting to play combat music!");

                    // TODO: still in testing
                    Map map = maps[index];
                    MapComponent_FogGrid? fog = map.GetComp_Fast<MapComponent_FogGrid>();
                    if (fog is not null)
                    {
                        // TODO: maybe cache hostile pawns?
                        __result = map.mapPawns.AllPawnsSpawned.Any(p => p.HostileTo(Faction.OfPlayerSilentFail) && !fog.IsFogged(p.Position));
                        Logger.LogVerbose($"allowing combat music? {__result}");
                    }
                    else
                    {
                        __result = true;
                        Logger.LogVerbose($"fog was null :C allowing default behavior...");
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
