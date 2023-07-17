using HarmonyLib;
using PacketLoss.Audit.Chains;
using PacketLoss.Audit.Presets;
using PacketLoss.HarmonyPatches;
using RimWorld;
using UnityEngine;
using Verse;

namespace PacketLoss;

public class PacketLossMod : Mod
{
    public static PacketLossSettings Settings { get; private set; } = null!;

    public PacketLossMod(ModContentPack content) : base(content)
    {
        Log.Message($"Loading {nameof(PacketLossMod)}...");
        Settings = GetSettings<PacketLossSettings>();
        IRuleChain chain = DefaultChainProvider.GetChain();
        LetterStack_ReceiveLetterPatch.UseRuleChain(chain);
        new Harmony("Th3Fr3d.PacketLoss").PatchAll();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        if (Settings is null)
        {
            Logger.Error("Settings was null!");
            throw new ArgumentNullException(nameof(Settings));
        }
        Listing_Standard list = new();
        list.Begin(inRect);
        list.Label("General settings");
        list.CheckboxLabeled("Is Active", ref Settings.isActive);
        list.CheckboxLabeled("Enable Logging", ref Settings.enableLogging);
        list.CheckboxLabeled("Enable verbose logging", ref Settings.enableVerboseLogging);
        list.GapLine();
        list.Label("Rules");
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.ThreatBig)}-letters", ref Settings.dropThreatBig);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.ThreatSmall)}-letters", ref Settings.dropThreatSmall);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.NegativeEvent)}-letters", ref Settings.dropNegativeEvent);
        list.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory() => "Packet Loss";
}
