using Verse;

namespace PostOffice.Audit.Rules;

internal abstract class OptionalRule : BaseRule
{
    private readonly Func<PostOfficeSettings, bool> _isEnabled;

    protected OptionalRule(Func<PostOfficeSettings, bool> isEnabled, string? debugName = null) : base(debugName)
    {
        _isEnabled = isEnabled;
    }

    public override bool CanHandle(Letter letter) =>
        _isEnabled.Invoke(PostOfficeMod.Settings);
}
