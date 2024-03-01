using System.Linq;
using Verse;

namespace PostOffice;

internal static class ModDependency
{
    public const string CAI5000 = "Krkr.rule56";

    public static bool IsAvailable(string modId) =>
        LoadedModManager.RunningMods.Any(mod =>
            mod.PackageIdPlayerFacing?.Equals(modId) is true);
}
