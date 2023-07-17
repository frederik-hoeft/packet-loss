using PacketLoss.Audit.Chains;
using PacketLoss.Audit.Rules;
using RimWorld;

namespace PacketLoss.Audit.Presets;

internal static class DefaultChainProvider
{
    public static IRuleChain GetChain()
    {
        IRuleChain chain = new RuleChain
        {
            DefaultAction = MessageAction.Forward
        };
        chain.Add(Rule.DropIfMatches(LetterDefOf.ThreatBig, settings => settings.dropThreatBig, "ThreatBig"));
        chain.Add(Rule.DropIfMatches(LetterDefOf.ThreatSmall, settings => settings.dropThreatSmall, "ThreatSmall"));
        chain.Add(Rule.DropIfMatches(LetterDefOf.NegativeEvent, settings => settings.dropNegativeEvent, "NegativeEvent"));
        return chain;
    }
}
