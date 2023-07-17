using Verse;

namespace PacketLoss.Audit.Rules;

internal class DefMatchDropRule : DefMatchRule
{
    internal DefMatchDropRule(LetterDef letterDef, Func<PacketLossSettings, bool> isEnabled, string? debugName = null) 
        : base(letterDef, isEnabled, debugName)
    {
    }

    public override MessageAction Audit(Letter letter) => 
        letter.def.Equals(LetterDef) ? MessageAction.Drop : MessageAction.Forward;
}
