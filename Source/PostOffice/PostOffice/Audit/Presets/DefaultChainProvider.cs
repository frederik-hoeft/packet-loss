using PostOffice.Audit.Chains;
using PostOffice.Audit.Rules;
using RimWorld;

namespace PostOffice.Audit.Presets;

internal static class DefaultChainProvider
{
    public static IRuleChain GetChain()
    {
        IRuleChain chain = new RuleChain
        {
            DefaultAction = MessageAction.Forward
        };
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.ThreatBig, settings => settings.dropThreatBig, "ThreatBig"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.ThreatSmall, settings => settings.dropThreatSmall, "ThreatSmall"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.AcceptJoiner, settings => settings.dropAcceptJoiner, "AcceptJoiner"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.AcceptVisitors, settings => settings.dropAcceptVisitors, "AcceptVisitors"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.BabyBirth, settings => settings.dropBabyBirth, "BabyBirth"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.BabyToChild, settings => settings.dropBabyToChild, "BabyToChild"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.BetrayVisitors, settings => settings.dropBetrayVisitors, "BetrayVisitors"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.Bossgroup, settings => settings.dropBossgroup, "Bossgroup"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.BundleLetter, settings => settings.dropBundleLetter, "BundleLetter"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.ChildBirthday, settings => settings.dropChildBirthday, "ChildBirthday"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.ChildToAdult, settings => settings.dropChildToAdult, "ChildToAdult"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.ChoosePawn, settings => settings.dropChoosePawn, "ChoosePawn"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.Death, settings => settings.dropDeath, "Death"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.NegativeEvent, settings => settings.dropNegativeEvent, "NegativeEvent"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.NeutralEvent, settings => settings.dropNeutralEvent, "NeutralEvent"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.NewQuest, settings => settings.dropNewQuest, "NewQuest"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.PositiveEvent, settings => settings.dropPositiveEvent, "PositiveEvent"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.RelicHuntInstallationFound, settings => settings.dropRelicHuntInstallationFound, "RelicHuntInstallationFound"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.RitualOutcomeNegative, settings => settings.dropRitualOutcomeNegative, "RitualOutcomeNegative"));
        chain.Add(Rule.DropIfMatches(() => LetterDefOf.RitualOutcomePositive, settings => settings.dropRitualOutcomePositive, "RitualOutcomePositive"));
        return chain;
    }
}
