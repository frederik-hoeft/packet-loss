using HarmonyLib;
using PostOffice.Audit;
using PostOffice.Audit.Chains;
using System.Diagnostics.CodeAnalysis;
using Verse;

namespace PostOffice.Patching.HarmonyPatches;

using static PostOfficeMod;

[HarmonyPatch(typeof(Messages), nameof(Messages.Message), typeof(Message), typeof(bool))]
public static class Messages_MessagePatch
{
    private static IRuleChain<Message>? _ruleChain;

    public static void UseRuleChain(IRuleChain<Message> ruleChain) =>
        _ruleChain = ruleChain;

    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Method signature must match original method.")]
    public static bool Prefix(Message msg, bool historical = true)
    {
        if (Settings?.isActive is true)
        {
            AssertRuleChain();
            Logger.LogVerbose($"intercepted {nameof(Messages)}.{nameof(Messages.Message)}() using {nameof(Messages_MessagePatch)} for message type '{msg?.def?.defName ?? "<Unknown>"}'.");

            // quick filter, if CanShowInLetterStack is false, then the letter won't be shown anyway
            if (msg is { text: not null and not "" } && _ruleChain!.CanHandle(msg))
            {
                ChainAction action = _ruleChain.Audit(msg);
                return action is ChainAction.Forward;
            }
        }
        return true;
    }
    private static void AssertRuleChain() =>
        _ = _ruleChain ?? throw new InvalidOperationException($"[PostOffice] {nameof(LetterStack_ReceiveLetterPatch)} attempted to apply rule chain, but chain was null :C");
}
