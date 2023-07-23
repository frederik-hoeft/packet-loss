using PostOffice.Audit.Chains;
using System.Collections.Generic;
using Verse;

namespace PostOffice.Audit.Rules.Letters;

internal class LetterRuleChain : RuleChain<Letter>
{
    public LetterRuleChain()
    {
    }

    public LetterRuleChain(List<IRule<Letter>> rules) : base(rules)
    {
    }

    protected override string TargetTypeName => nameof(Letter);

    public override bool CanHandle(Letter target) => true;
}
