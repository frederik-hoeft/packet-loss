namespace PostOffice.Audit.Rules;

internal abstract class BaseRule<TTarget>(string? debugName) : IRule<TTarget>
{
    public string? DebugName { get; } = debugName;

    public abstract ChainAction Audit(TTarget target);

    public abstract bool CanHandle(TTarget target);
}
