using System.Collections.Generic;
using PostOffice.Audit.Rules;
using Verse;

namespace PostOffice.Audit.Chains;

public class RuleChain : IRuleChain
{
    private readonly List<IRule> _rules;

    public RuleChain()
    {
        _rules = new List<IRule>();
    }

    public RuleChain(List<IRule> rules) => _rules = rules;

    public MessageAction DefaultAction { get; set; } = MessageAction.Forward;

    public void Add(IRule rule) => _rules.Add(rule);

    public MessageAction Audit(Letter letter)
    {
        MessageAction result;
        foreach (IRule rule in _rules)
        {
            if (rule.CanHandle(letter))
            {
                result = rule.Audit(letter);
                if (result != MessageAction.NextHandler)
                {
                    Logger.Log($"Rule {rule.GetType().Name}::{(rule as OptionalRule)?.DebugName ?? "<UnknownRule>"} matched letter. The following action will be taken: {result}");
                    return result;
                }
            }
        }
        result = DefaultAction;
        Logger.LogVerbose($"No rule matched letter. Defaulting to: {result}");
        return result;
    }

    public bool CanHandle(Letter letter) => true;
}
