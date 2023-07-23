using PostOffice.Audit.Chains;
using PostOffice.Audit.Rules.Letters;
using RimWorld;
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
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.ThreatBig, settings => settings.dropThreatBig, "ThreatBig"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.ThreatSmall, settings => settings.dropThreatSmall, "ThreatSmall"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.AcceptJoiner, settings => settings.dropAcceptJoiner, "AcceptJoiner"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.AcceptVisitors, settings => settings.dropAcceptVisitors, "AcceptVisitors"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.BabyBirth, settings => settings.dropBabyBirth, "BabyBirth"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.BabyToChild, settings => settings.dropBabyToChild, "BabyToChild"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.BetrayVisitors, settings => settings.dropBetrayVisitors, "BetrayVisitors"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.Bossgroup, settings => settings.dropBossgroup, "Bossgroup"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.BundleLetter, settings => settings.dropBundleLetter, "BundleLetter"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.ChildBirthday, settings => settings.dropChildBirthday, "ChildBirthday"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.ChildToAdult, settings => settings.dropChildToAdult, "ChildToAdult"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.ChoosePawn, settings => settings.dropChoosePawn, "ChoosePawn"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.Death, settings => settings.dropDeath, "Death"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.NegativeEvent, settings => settings.dropNegativeEvent, "NegativeEvent"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.NeutralEvent, settings => settings.dropNeutralEvent, "NeutralEvent"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.NewQuest, settings => settings.dropNewQuest, "NewQuest"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.PositiveEvent, settings => settings.dropPositiveEvent, "PositiveEvent"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.RelicHuntInstallationFound, settings => settings.dropRelicHuntInstallationFound, "RelicHuntInstallationFound"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.RitualOutcomeNegative, settings => settings.dropRitualOutcomeNegative, "RitualOutcomeNegative"));
        chain.Add(LetterRule.DropIfMatches(() => LetterDefOf.RitualOutcomePositive, settings => settings.dropRitualOutcomePositive, "RitualOutcomePositive"));
        return chain;
    }
}
