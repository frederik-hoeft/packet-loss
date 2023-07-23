namespace PostOffice.Audit.Rules;

public interface IRule<TTarget>
{
    bool CanHandle(TTarget target);

    ChainAction Audit(TTarget target);
}
