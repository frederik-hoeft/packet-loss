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
        Settings = GetSettings<PacketLossSettings>();
        IRuleChain chain = DefaultChainProvider.GetChain();
        LetterStack_ReceiveLetterPatch.UseRuleChain(chain);
        new Harmony("Th3Fr3d.PacketLoss").PatchAll();
        Log.Message($"Initialized {nameof(PacketLossMod)}!");
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
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.AcceptJoiner)}-letters", ref Settings.dropAcceptJoiner);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.AcceptVisitors)}-letters", ref Settings.dropAcceptVisitors);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.BabyToChild)}-letters", ref Settings.dropBabyToChild);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.BabyBirth)}-letters", ref Settings.dropBabyBirth);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.BetrayVisitors)}-letters", ref Settings.dropBetrayVisitors);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.Bossgroup)}-letters", ref Settings.dropBossgroup);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.BundleLetter)}-letters", ref Settings.dropBundleLetter);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.ChildBirthday)}-letters", ref Settings.dropChildBirthday);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.ChildToAdult)}-letters", ref Settings.dropChildToAdult);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.Death)}-letters", ref Settings.dropDeath);
        //list.CheckboxLabeled($"Drop {nameof(LetterDefOf.ChoosePawn)}-letters", ref Settings.dropChoosePawn);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.NegativeEvent)}-letters", ref Settings.dropNegativeEvent);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.NeutralEvent)}-letters", ref Settings.dropNeutralEvent);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.NewQuest)}-letters", ref Settings.dropNewQuest);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.PositiveEvent)}-letters", ref Settings.dropPositiveEvent);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.RelicHuntInstallationFound)}-letters", ref Settings.dropRelicHuntInstallationFound);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.RitualOutcomeNegative)}-letters", ref Settings.dropRitualOutcomeNegative);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.RitualOutcomePositive)}-letters", ref Settings.dropRitualOutcomePositive);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.ThreatBig)}-letters", ref Settings.dropThreatBig);
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.ThreatSmall)}-letters", ref Settings.dropThreatSmall);
        list.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory() => "Packet Loss";
}
