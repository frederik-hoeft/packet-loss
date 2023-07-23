using PostOffice.Audit.Chains;
using PostOffice.Audit.Rules.Messages;
using RimWorld;
using Verse;

namespace PostOffice.Audit.Presets;

internal static class MessageChainProvider
{
    public static IRuleChain<Message> GetChain()
    {
        IRuleChain<Message> chain = new MessageRuleChain
        {
            DefaultAction = ChainAction.Forward
        };
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.ThreatBig, settings => settings.dropThreatBig, "ThreatBig"));
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.ThreatSmall, settings => settings.dropThreatSmall, "ThreatSmall"));
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.NegativeEvent, settings => settings.dropNegativeEvent, "NegativeEvent"));
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.NeutralEvent, settings => settings.dropNeutralEvent, "NeutralEvent"));
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.PawnDeath, settings => settings.dropDeath, "PawnDeath"));
        return chain;
    }
}
