using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Linq;
using CombatAI;

namespace PostOffice.Patching.HarmonyPatches;

using static PostOfficeMod;

[HarmonyPatch(typeof(MusicManagerPlay), nameof(MusicManagerPlay.DangerMusicMode), MethodType.Getter)]
[RequiresMod(ModDependency.CAI5000)]
public static class MusicManagerPlay_DangerMusicModePatch
{
    private static bool _previousResult;

    public static bool Prefix(MusicManagerPlay __instance, ref bool __result)
    {
        // override base game implementation only iff mod is enabled
        // and dependencies are loaded.
        if (Settings is { isActive: true, cai5000_delayCombatMusic: true} && ModDependency.IsCai5000Loaded)
        {
            // for every map check what the game thinks about the danger rating
            bool resultSet = false;
            List<Map> maps = Find.Maps;
            for (int index = 0; index < maps.Count; index++)
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
                        resultSet = true;
                        if (__result)
                        {
                            // prevent spamming the log
                            if (_previousResult != __result)
                            {
                                Logger.LogVerbose($"(CAI-5000 patch) DangerRating is high and RimWorld is attempting to play combat music! Threat is visible, so allowing combat music.");
                            }
                            _previousResult = __result;
                            // if we already found a visible threat, we can stop here
                            return false;
                        }
                        // prevent spamming the log
                        else if (_previousResult != __result)
                        {
                            _previousResult = __result;
                            Logger.LogVerbose($"(CAI-5000 patch) DangerRating is high and RimWorld is attempting to play combat music! Threat is not visible, so blocking combat music.");
                        }
                    }
                    else
                    {
                        // if fog of war is disabled or otherwise unavailable, allow default behavior (no need to continue checking)
                        __result = true;
                        return false;
                    }
                }
            }
            if (!resultSet)
            {
                // we didn't find any high danger maps.
                // the base game allows danger music to play if override is enabled.
                __result = __instance.OverrideDangerMode;
            }
            // no need to call into base game implementation (we basically did everything here)
            return false;
        }
        // mod is disabled or dependencies are not loaded
        // forward to base game implementation
        return true;
    }
}
