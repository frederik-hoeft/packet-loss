using HarmonyLib;
using PacketLoss.Audit;
using PacketLoss.Audit.Chains;
using Verse;

namespace PacketLoss.HarmonyPatches;

using static PacketLossMod;

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
            if (Settings.enableVerboseLogging)
            {
                Logger.Log($"intercepted LetterStack.ReceiveLetter() using {nameof(LetterStack_ReceiveLetterPatch)} for def {let?.def.ToStringSafe()}.");
            }
            if (let is { CanShowInLetterStack: true} && _ruleChain.CanHandle(let))
            {
                MessageAction action = _ruleChain.Audit(let);
                return action is MessageAction.Forward;
            }
        }
        return true;
    }
}