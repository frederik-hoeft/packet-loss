using PostOffice.Audit.Rules;

namespace PostOffice.Audit.Chains;

public interface IRuleChain : IRule
{
    void Add(IRule rule);

    MessageAction DefaultAction { get; set; }
}
