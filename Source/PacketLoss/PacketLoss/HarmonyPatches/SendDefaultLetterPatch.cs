using HarmonyLib;
using RimWorld;
using Verse;

namespace PacketLoss.HarmonyPatches;

using static PacketLossMod;

[HarmonyPatch(typeof(IncidentWorker), "SendStandardLetter",
    typeof(TaggedString), typeof(TaggedString), typeof(LetterDef), typeof(IncidentParms), typeof(LookTargets), typeof(NamedArgument[]))]
public static class SendDefaultLetterPatch
{
    public static bool Prefix(TaggedString baseLetterLabel,
        TaggedString baseLetterText,
        LetterDef baseLetterDef,
        IncidentParms parms,
        LookTargets lookTargets,
        NamedArgument[] textArgs)
    {
        Log.Message($"Triggered {nameof(SendDefaultLetterPatch)} for class {baseLetterDef?.letterClass}. Returning true...");
        return true;
        if (Settings?.isActive is true)
        {
            if (baseLetterDef is null)
            {
                Log.Warning($"{nameof(baseLetterDef)} was null.");
                return true;
            }
            // process rules
            return baseLetterDef.AuditRule(Settings.dropThreatBig, LetterDefOf.ThreatBig, nameof(LetterDefOf.ThreatBig))
                && baseLetterDef.AuditRule(Settings.dropThreatSmall, LetterDefOf.ThreatSmall, nameof(LetterDefOf.ThreatSmall))
                && baseLetterDef.AuditRule(Settings.dropNegativeEvent, LetterDefOf.AcceptJoiner, nameof(LetterDefOf.AcceptJoiner));
        }
        Log.Message("This is fine!");
        return true;
    }

    /// <summary>
    /// returns <see langword="true"/> if the letter is allowed to be sent according to the current rule, otherwise <see langword="false"/>.
    /// </summary>
    private static bool AuditRule(this LetterDef letterDef, bool ruleEnabled, LetterDef forbiddenLetter, string? ruleName = null)
    {
        if (ruleEnabled && letterDef.Equals(forbiddenLetter))
        {
            Log.Message($"Dropped letter because '{ruleName ?? "<Unknown>"}' letters are disallowed by the current settings!");
            return false;
        }
        return true;
    }
}
