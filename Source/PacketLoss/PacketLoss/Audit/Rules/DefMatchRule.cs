using Verse;

namespace PacketLoss.Audit.Rules;

internal abstract class DefMatchRule : OptionalRule
{
    protected LetterDef LetterDef { get; }

    protected DefMatchRule(LetterDef letterDef, Func<PacketLossSettings, bool> isEnabled, string? debugName = null) : base(isEnabled, debugName)
    {
        LetterDef = letterDef;
    }

    public override bool CanHandle(Letter letter) => base.CanHandle(letter) && letter.def is not null;
}
