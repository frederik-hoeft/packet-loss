using Verse;

namespace PostOffice.Audit.Rules.Letters;

internal class LetterDefMatchRule : DefMatchRule<Letter, LetterDef>
{
    public LetterDefMatchRule(Func<LetterDef> defSupplier, ChainAction matchAction, Func<PostOfficeSettings, bool> isEnabled, string? debugName = null) 
        : base(defSupplier, matchAction, isEnabled, debugName)
    {
    }

    protected override LetterDef? GetDefOf(Letter target) => target.def;
}
