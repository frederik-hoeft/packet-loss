using HarmonyLib;
using PostOffice.Audit;
using PostOffice.Audit.Chains;
using Verse;

namespace PostOffice.HarmonyPatches;

using static PostOfficeMod;

[HarmonyPatch(typeof(LetterStack), nameof(LetterStack.ReceiveLetter), typeof(Letter), typeof(string))]
public static class LetterStack_ReceiveLetterPatch
{
    private static IRuleChain? _ruleChain;

    public static void UseRuleChain(IRuleChain ruleChain) =>
        _ruleChain = ruleChain;

    public static bool Prefix(Letter let, string? debugInfo = null)
    {
        if (Settings?.isActive is true)
        {
            _ = _ruleChain ?? throw new InvalidOperationException($"LetterStack_ReceiveLetterPatch attempted to apply rule chain, but chain was null :C");
            Logger.LogVerbose($"intercepted LetterStack.ReceiveLetter() using {nameof(LetterStack_ReceiveLetterPatch)} for letter type '{let?.def?.defName ?? "<Unknown>"}'.");
            // quick filter, if CanShowInLetterStack is false, then the letter won't be shown anyway
            if (let is { CanShowInLetterStack: true} && _ruleChain.CanHandle(let))
            {
                MessageAction action = _ruleChain.Audit(let);
                return action is MessageAction.Forward;
            }
        }
        return true;
    }
}