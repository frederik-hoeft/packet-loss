using PacketLoss.Audit.Rules;

namespace PacketLoss.Audit.Chains;

public interface IRuleChain : IRule
{
    void Add(IRule rule);

    MessageAction DefaultAction { get; set; }
}
