using HarmonyLib;
using RimWorld;

namespace PostOffice.Patching.HarmonyPatches;

using static PostOfficeMod;

[HarmonyPatch(typeof(MusicManagerPlay), nameof(MusicManagerPlay.MusicUpdate))]
[RequiresMod("Krkr.rule56")]
public static class MusicManagerPlay_MusicUpdatePatch
{
    private static int _skippedTicks = 0;

    private const int TICK_DELTA = 100;

    public static bool Prefix()
    {
        if (Settings?.isActive is true)
        {
            if (_skippedTicks++ >= TICK_DELTA)
            {
                _skippedTicks = 0;
                return true;
            }
            return false;
        }
        return true;
    }
}
