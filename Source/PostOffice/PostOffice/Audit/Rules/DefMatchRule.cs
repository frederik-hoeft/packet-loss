using Verse;

namespace PostOffice.Audit.Rules;

internal abstract class DefMatchRule<TTarget, TDef> : OptionalRule<TTarget> where TDef : Def
{
    private readonly Func<TDef> _defSupplier;
    private readonly ChainAction _matchAction;

    public DefMatchRule(Func<TDef> defSupplier, ChainAction matchAction, Func<PostOfficeSettings, bool> isEnabled, string? debugName = null) 
        : base(isEnabled, debugName)
    {
        _defSupplier = defSupplier;
        _matchAction = matchAction;
    }

    protected abstract TDef? GetDefOf(TTarget target);

    public override bool CanHandle(TTarget target) => 
        base.CanHandle(target) && !string.IsNullOrEmpty(GetDefOf(target)?.defName);

    public override ChainAction Audit(TTarget target) =>
        GetDefOf(target)!.defName.Equals(_defSupplier().defName, StringComparison.InvariantCultureIgnoreCase) ? _matchAction : ChainAction.NextHandler;
}
