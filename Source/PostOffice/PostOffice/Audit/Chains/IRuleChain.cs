using PostOffice.Audit.Rules;

namespace PostOffice.Audit.Chains;

public interface IRuleChain<TTarget> : IRule<TTarget>
{
    void Add(IRule<TTarget> rule);

    ChainAction DefaultAction { get; set; }
}
