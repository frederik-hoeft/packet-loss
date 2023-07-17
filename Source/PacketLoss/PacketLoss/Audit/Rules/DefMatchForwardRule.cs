using Verse;

namespace PacketLoss.Audit.Rules;

internal class DefMatchForwardRule : DefMatchRule
{
    internal DefMatchForwardRule(LetterDef letterDef, Func<PacketLossSettings, bool> isEnabled, string? debugName = null) 
        : base(letterDef, isEnabled, debugName)
    {
    }

    public override MessageAction Audit(Letter letter) => 
        letter.def.Equals(LetterDef) ? MessageAction.Forward : MessageAction.Drop;
}
