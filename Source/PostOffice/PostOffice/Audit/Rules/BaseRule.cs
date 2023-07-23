using Verse;

namespace PostOffice.Audit.Rules;

internal abstract class BaseRule<TTarget> : IRule<TTarget>
{
    public string? DebugName { get; }

    protected BaseRule(string? debugName) => DebugName = debugName;

    public abstract ChainAction Audit(TTarget target);

    public abstract bool CanHandle(TTarget target);
}
