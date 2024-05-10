using PostOffice.Audit.Rules;
using PostOffice.Dependencies;

namespace PostOffice.Audit.Chains;

public interface IRuleChain<TTarget> : IRule<TTarget>
{
    void Add(IRule<TTarget> rule);

    void Add<TModDependency>(IRule<TTarget> rule)
        where TModDependency : struct, IRequiresMod;

    ChainAction DefaultAction { get; set; }
}
