using PostOffice.Audit.Chains;
using PostOffice.Audit.Rules.Letters;
using PostOffice.Audit.Rules.Messages;
using RimWorld;
using System.Text.RegularExpressions;
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
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.ThreatBig, settings => settings.dropThreatBig, nameof(MessageTypeDefOf.ThreatBig)));
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.ThreatSmall, settings => settings.dropThreatSmall, nameof(MessageTypeDefOf.ThreatSmall)));
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.NegativeEvent, settings => settings.dropNegativeEvent, nameof(MessageTypeDefOf.NegativeEvent)));
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.NegativeHealthEvent, settings => settings.dropNegativeEvent, nameof(MessageTypeDefOf.NegativeHealthEvent)));
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.NeutralEvent, settings => settings.dropNeutralEvent, nameof(MessageTypeDefOf.NeutralEvent)));
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.PawnDeath, settings => settings.dropDeath, nameof(MessageTypeDefOf.PawnDeath)));
        chain.Add(MessageRule.DropIfMatches(() => MessageTypeDefOf.PositiveEvent, settings => settings.dropPositiveEvent, nameof(MessageTypeDefOf.PositiveEvent)));
        // dynamic
        chain.Add(MessageRule.Dynamic(message => (message.text, PostOfficeMod.Settings.DropMessageRegexCompiled) switch
        {
            (string label, Regex validRegex) when validRegex.IsMatch(label) => ChainAction.Drop,
            _ => ChainAction.NextHandler
        }, settings => settings.DropRegex is not null, "DropByMessageRegex"));
        return chain;
    }
}
