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
        if (Settings?.isActive is true)
        {
            Logger.LogVerbose($"intercepted {nameof(MusicManagerPlay)}.DangerMusicMode_get() using {nameof(MusicManagerPlay_DangerMusicModePatch)}.");
            List<Map> maps = Find.Maps;
            for (int index = 0; index < maps.Count; ++index)
            {
                if (maps[index].dangerWatcher.DangerRating == StoryDanger.High)
                {
                    Logger.LogVerbose($"DangerRating is high. RimWorld is attempting to play combat music!");
                    // TODO: now actually check if threat is visible in fog of war
                    // TODO: is there an API for that?
                    // maybe something like:
                    SightTracker tracker = maps[index].GetComp_Fast<SightTracker>();
                    tracker.TryGetReader(Faction.OfPlayerSilentFail, out SightTracker.SightReader? reader);
                    Logger.LogVerbose($"there may be {reader.hostiles?.Length} hostiles visible on the map right now. Is that true?");
                    
                    __result = true;
                    return false;
                }
            }
            __result = false;
            return false;
        }
        return true;
    }
}
