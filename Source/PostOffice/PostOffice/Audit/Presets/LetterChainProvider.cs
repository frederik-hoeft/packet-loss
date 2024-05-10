using PostOffice.Audit.Chains;
using PostOffice.Audit.Rules.Letters;
using PostOffice.Dependencies.BuiltIn;
using RimWorld;
using System.Text.RegularExpressions;
using Verse;

namespace PostOffice.Audit.Presets;

internal static class LetterChainProvider
{
    public static IRuleChain<Letter> GetChain()
    {
        IRuleChain<Letter> chain = new LetterRuleChain
        {
            DefaultAction = ChainAction.Forward
        };
        // Core LetterDefs
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.AcceptJoiner, settings => settings.dropAcceptJoiner, nameof(LetterDefOf.AcceptJoiner)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.AcceptVisitors, settings => settings.dropAcceptVisitors, nameof(LetterDefOf.AcceptVisitors)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.BundleLetter, settings => settings.dropBundleLetter, nameof(LetterDefOf.BundleLetter)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.ChoosePawn, settings => settings.dropChoosePawn, nameof(LetterDefOf.ChoosePawn)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.Death, settings => settings.dropDeath, nameof(LetterDefOf.Death)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.NegativeEvent, settings => settings.dropNegativeEvent, nameof(LetterDefOf.NegativeEvent)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.NeutralEvent, settings => settings.dropNeutralEvent, nameof(LetterDefOf.NeutralEvent)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.PositiveEvent, settings => settings.dropPositiveEvent, nameof(LetterDefOf.PositiveEvent)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.RitualOutcomeNegative, settings => settings.dropRitualOutcomeNegative, nameof(LetterDefOf.RitualOutcomeNegative)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.RitualOutcomePositive, settings => settings.dropRitualOutcomePositive, nameof(LetterDefOf.RitualOutcomePositive)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.ThreatBig, settings => settings.dropThreatBig, nameof(LetterDefOf.ThreatBig)));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.ThreatSmall, settings => settings.dropThreatSmall, nameof(LetterDefOf.ThreatSmall)));
        // Ideology LetterDefs
        chain.Add<RequiresIdeology>(LetterRule.DropIfMatches(() => LetterDefOf.RelicHuntInstallationFound, settings => settings.dropRelicHuntInstallationFound, nameof(LetterDefOf.RelicHuntInstallationFound)));
        // Biotech LetterDefs
        chain.Add<RequiresBiotech>(LetterRule.DropIfMatches(() => LetterDefOf.BabyBirth, settings => settings.dropBabyBirth, nameof(LetterDefOf.BabyBirth)));
        chain.Add<RequiresBiotech>(LetterRule.DropIfMatches(() => LetterDefOf.BabyToChild, settings => settings.dropBabyToChild, nameof(LetterDefOf.BabyToChild)));
        chain.Add<RequiresBiotech>(LetterRule.DropIfMatches(() => LetterDefOf.Bossgroup, settings => settings.dropBossgroup, nameof(LetterDefOf.Bossgroup)));
        chain.Add<RequiresBiotech>(LetterRule.DropIfMatches(() => LetterDefOf.ChildBirthday, settings => settings.dropChildBirthday, nameof(LetterDefOf.ChildBirthday)));
        chain.Add<RequiresBiotech>(LetterRule.DropIfMatches(() => LetterDefOf.ChildToAdult, settings => settings.dropChildToAdult, nameof(LetterDefOf.ChildToAdult)));
        // Anomaly LetterDefs
        chain.Add<RequiresAnomaly>(LetterRule.DropIfMatches(() => LetterDefOf.AcceptCreepJoiner, settings => settings.dropAcceptCreepJoiner, nameof(LetterDefOf.AcceptCreepJoiner)));
        chain.Add<RequiresAnomaly>(LetterRule.DropIfMatches(() => LetterDefOf.EntityDiscovered, settings => settings.dropEntityDiscovered, nameof(LetterDefOf.EntityDiscovered)));
        // dynamic
        chain.Add(LetterRule.Dynamic(letter => (letter.Label.RawText, PostOfficeMod.Settings.DropRegexCompiled) switch
        {
            (string label, Regex validRegex) when validRegex.IsMatch(label) => ChainAction.Drop,
            _ => ChainAction.NextHandler
        }, settings => settings.DropRegex is not null, "DropByRegex"));
        return chain;
    }
}
