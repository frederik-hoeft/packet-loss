using Verse;

namespace PostOffice.Audit.Rules;

internal static class Rule
{
    public static IRule Dynamic(Func<Letter, MessageAction> ruleDefinition, Func<PostOfficeSettings, bool>? isEnabled = null, string? debugName = null) =>
        new DynamicRule(letter => ruleDefinition(letter), isEnabled ?? True, debugName);

    public static IRule DropIfMatches(Func<LetterDef> defSupplier, Func<PostOfficeSettings, bool>? isEnabled = null, string? debugName = null) =>
        new DefMatchRule(defSupplier, MessageAction.Drop, isEnabled ?? True, debugName);

    public static IRule ForwardIfMatches(Func<LetterDef> defSupplier, Func<PostOfficeSettings, bool>? isEnabled = null, string? debugName = null) =>
        new DefMatchRule(defSupplier, MessageAction.Forward, isEnabled ?? True, debugName);

    private static bool True(PostOfficeSettings _) => true;
}
