using System.Collections.Generic;
using PostOffice.Audit.Rules;
using PostOffice.Dependencies;

namespace PostOffice.Audit.Chains;

public abstract class RuleChain<TTarget> : IRuleChain<TTarget>
{
    private readonly List<IRule<TTarget>> _rules;

    protected RuleChain() => _rules = [];

    public RuleChain(List<IRule<TTarget>> rules) => _rules = rules;

    protected abstract string TargetTypeName { get; }

    public ChainAction DefaultAction { get; set; } = ChainAction.Forward;

    public void Add(IRule<TTarget> rule) => _rules.Add(rule);

    public void Add<TModDependency>(IRule<TTarget> rule) where TModDependency : struct, IRequiresMod
    {
        if (ModDependency.IsSatisfied<TModDependency>())
        {
            Add(rule);
        }
    }

    public ChainAction Audit(TTarget target)
    {
        ChainAction result;
        foreach (IRule<TTarget> rule in _rules)
        {
            if (rule.CanHandle(target))
            {
                result = rule.Audit(target);
                if (result != ChainAction.NextHandler)
                {
                    Logger.Log($"Rule {rule.GetType().Name}::{(rule as OptionalRule<TTarget>)?.DebugName ?? "<UnknownRule>"} matched {TargetTypeName}. " +
                        $"The following action will be taken: {result}");
                    return result;
                }
            }
        }
        result = DefaultAction;
        Logger.LogVerbose($"No rule matched {TargetTypeName}. Defaulting to: {result}");
        return result;
    }

    public abstract bool CanHandle(TTarget target);
}
