using PostOffice.Audit.Chains;
using System.Collections.Generic;
using Verse;

namespace PostOffice.Audit.Rules.Messages;

internal class MessageRuleChain : RuleChain<Message>
{
    public MessageRuleChain()
    {
    }

    public MessageRuleChain(List<IRule<Message>> rules) : base(rules)
    {
    }

    protected override string TargetTypeName => nameof(Message);

    public override bool CanHandle(Message target) => true;
}
