using HarmonyLib;
using PostOffice.Audit;
using PostOffice.Audit.Chains;
using Verse;

namespace PostOffice.Patching.HarmonyPatches;

using static PostOfficeMod;

[HarmonyPatch(typeof(Messages), nameof(Messages.Message), typeof(Message), typeof(bool))]
public static class Messages_MessagePatch
{
    private static IRuleChain<Message>? _ruleChain;

    public static void UseRuleChain(IRuleChain<Message> ruleChain) =>
        _ruleChain = ruleChain;

#pragma warning disable IDE0060 // Remove unused parameter
    public static bool Prefix(Message msg, bool historical = true)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        if (Settings?.isActive is true)
        {
            _ = _ruleChain ?? throw new InvalidOperationException($"{nameof(Messages_MessagePatch)} attempted to apply rule chain, but chain was null :C");
            Logger.LogVerbose($"intercepted {nameof(Messages)}.{nameof(Messages.Message)}() using {nameof(Messages_MessagePatch)} for message type '{msg?.def?.defName ?? "<Unknown>"}'.");

            // quick filter, if CanShowInLetterStack is false, then the letter won't be shown anyway
            if (msg is { text: not null and not "" } && _ruleChain.CanHandle(msg))
            {
                ChainAction action = _ruleChain.Audit(msg);
                return action is ChainAction.Forward;
            }
        }
        return true;
    }
}
