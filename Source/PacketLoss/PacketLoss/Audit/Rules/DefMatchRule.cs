using Verse;

namespace PacketLoss.Audit.Rules;

internal class DefMatchRule : OptionalRule
{
    private readonly Func<LetterDef> _letterDefSupplier;
    private readonly MessageAction _matchAction;

    public DefMatchRule(Func<LetterDef> letterDefSupplier, MessageAction matchAction, Func<PacketLossSettings, bool> isEnabled, string? debugName = null) : base(isEnabled, debugName)
    {
        _letterDefSupplier = letterDefSupplier;
        _matchAction = matchAction;
    }

    public override bool CanHandle(Letter letter) => base.CanHandle(letter) && !string.IsNullOrEmpty(letter.def?.defName);

    public override MessageAction Audit(Letter letter) =>
        letter.def.defName.Equals(_letterDefSupplier().defName, StringComparison.InvariantCultureIgnoreCase) ? _matchAction : MessageAction.NextHandler;
}
