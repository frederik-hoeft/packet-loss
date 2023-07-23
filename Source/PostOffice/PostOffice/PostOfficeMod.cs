using HarmonyLib;
using PostOffice.Audit.Chains;
using PostOffice.Audit.Presets;
using PostOffice.HarmonyPatches;
using RimWorld;
using UnityEngine;
using Verse;

namespace PostOffice;

public class PostOfficeMod : Mod
{
    public static PostOfficeSettings Settings { get; private set; } = null!;

    public PostOfficeMod(ModContentPack content) : base(content)
    {
        Settings = GetSettings<PostOfficeSettings>();

        IRuleChain<Letter> letterChain = LetterChainProvider.GetChain();
        LetterStack_ReceiveLetterPatch.UseRuleChain(letterChain);

        IRuleChain<Message> messageChain = MessageChainProvider.GetChain();
        Messages_MessagePatch.UseRuleChain(messageChain);

        new Harmony("Th3Fr3d.PostOffice").PatchAll();
        Log.Message($"Initialized {nameof(PostOfficeMod)}!");
    }

    public override void DoSettingsWindowContents(Rect canvas)
    {
        if (Settings is null)
        {
            Logger.Error("Settings was null!");
            throw new ArgumentNullException(nameof(Settings));
        }
        Listing_Standard list = new()
        {
            ColumnWidth = (canvas.width - 17) / 2
        };
        list.Begin(canvas);
        Text.Font = GameFont.Medium;
        list.Label("General settings");
        Text.Font = GameFont.Small;
        list.CheckboxLabeled("Is Active", ref Settings.isActive);
        list.CheckboxLabeled("Enable message support", ref Settings.enableMessageSupport, 
            "Also prevents messages in the top left from being shown if their attributes match the provided filters");
        list.CheckboxLabeled("Enable Logging", ref Settings.enableLogging);
        list.CheckboxLabeled("Enable verbose logging", ref Settings.enableVerboseLogging);
        list.NewColumn();
        Text.Font = GameFont.Medium;
        list.Label("Rules");
        Text.Font = GameFont.Small;
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
        list.CheckboxLabeled($"Drop {nameof(LetterDefOf.ChoosePawn)}-letters", ref Settings.dropChoosePawn);
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
        base.DoSettingsWindowContents(canvas);
    }

    public override string SettingsCategory() => "Post Office";
}
