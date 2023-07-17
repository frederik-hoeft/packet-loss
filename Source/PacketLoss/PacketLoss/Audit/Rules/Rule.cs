using Verse;

namespace PacketLoss.Audit.Rules;

internal static class Rule
{
    public static IRule DropIf(Func<Letter, bool> predicate, Func<PacketLossSettings, bool>? isEnabled = null, string? debugName = null) =>
        new DynamicRule(letter => predicate(letter) is false, isEnabled ?? True, debugName);

    public static IRule ForwardIf(Func<Letter, bool> predicate, Func<PacketLossSettings, bool>? isEnabled = null, string? debugName = null) =>
        new DynamicRule(predicate, isEnabled ?? True, debugName);

    public static IRule DropIfMatches(LetterDef def, Func<PacketLossSettings, bool>? isEnabled = null, string? debugName = null) =>
        new DefMatchDropRule(def, isEnabled ?? True, debugName);

    public static IRule ForwardIfMatches(LetterDef def, Func<PacketLossSettings, bool>? isEnabled = null, string? debugName = null) =>
        new DefMatchForwardRule(def, isEnabled ?? True, debugName);

    private static bool True(PacketLossSettings _) => true;
}
