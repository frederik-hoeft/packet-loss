using Verse;

namespace PacketLoss.Audit.Rules;

internal abstract class BaseRule : IRule
{
    public string? DebugName { get; }

    protected BaseRule(string? debugName) => DebugName = debugName;

    public abstract MessageAction Audit(Letter letter);

    public abstract bool CanHandle(Letter letter);
}
