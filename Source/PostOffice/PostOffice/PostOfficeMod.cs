using HarmonyLib;
using PostOffice.Audit.Chains;
using PostOffice.Audit.Presets;
using PostOffice.Patching;
using PostOffice.Patching.HarmonyPatches;
using RimWorld;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace PostOffice;

public class PostOfficeMod : Mod
{
    public static PostOfficeSettings Settings { get; private set; } = null!;
    private static int _foo = 0;

    public PostOfficeMod(ModContentPack content) : base(content)
    {
        Settings = GetSettings<PostOfficeSettings>();

        IRuleChain<Letter> letterChain = LetterChainProvider.GetChain();
        LetterStack_ReceiveLetterPatch.UseRuleChain(letterChain);

        IRuleChain<Message> messageChain = MessageChainProvider.GetChain();
        Messages_MessagePatch.UseRuleChain(messageChain);

        PostOfficePatches.Apply(new Harmony("Th3Fr3d.PostOffice"));
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
        list.CheckboxLabeled("Is active", ref Settings.isActive);
        list.CheckboxLabeled("Enable message support", ref Settings.enableMessageSupport, 
            "Also prevents messages in the top left from being shown if their attributes match the provided filters");
        list.CheckboxLabeled("Enable logging", ref Settings.enableLogging);
        list.CheckboxLabeled("Enable verbose logging", ref Settings.enableVerboseLogging);
        list.GapLine();
        Text.Font = GameFont.Medium;
        list.Label("Mod support");
        Text.Font = GameFont.Small;
        if (ModDependency.IsAvailable(ModDependency.CAI5000))
        {
            list.CheckboxLabeled("CAI-5000 Fog of War", ref Settings.cai5000_delayCombatMusic,
                $"If 'CAI 5000 - Advanced AI + Fog Of War' is loaded, {SettingsCategory()} will delay combat music until hostile forces have been detected by your colonists.");
        }
        list.NewColumn();
        Text.Font = GameFont.Medium;
        list.Label("Rules");
        Text.Font = GameFont.Small;
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.AcceptJoiner))}'", ref Settings.dropAcceptJoiner);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.AcceptVisitors))}'", ref Settings.dropAcceptVisitors);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.BabyToChild))}'", ref Settings.dropBabyToChild);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.BabyBirth))}'", ref Settings.dropBabyBirth);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.BetrayVisitors))}'", ref Settings.dropBetrayVisitors);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.Bossgroup))}'", ref Settings.dropBossgroup);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.BundleLetter))}'", ref Settings.dropBundleLetter);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.ChildBirthday))}'", ref Settings.dropChildBirthday);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.ChildToAdult))}'", ref Settings.dropChildToAdult);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.Death))}'", ref Settings.dropDeath);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.ChoosePawn))}'", ref Settings.dropChoosePawn);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.NegativeEvent))}'", ref Settings.dropNegativeEvent);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.NeutralEvent))}'", ref Settings.dropNeutralEvent);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.NewQuest))}'", ref Settings.dropNewQuest);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.PositiveEvent))}'", ref Settings.dropPositiveEvent);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.RelicHuntInstallationFound))}'", ref Settings.dropRelicHuntInstallationFound);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.RitualOutcomeNegative))}'", ref Settings.dropRitualOutcomeNegative);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.RitualOutcomePositive))}'", ref Settings.dropRitualOutcomePositive);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.ThreatBig))}'", ref Settings.dropThreatBig);
        list.CheckboxLabeled($"Hide letters of type '{PascalCaseToDisplayString(nameof(LetterDefOf.ThreatSmall))}'", ref Settings.dropThreatSmall);
        list.GapLine();
        list.Label("Hide letters by regular expression:");
        Settings.DropRegex = list.TextEntry(Settings.DropRegex);
        if (!Settings.DropRegexIsValid)
        {
            Text.Font = GameFont.Tiny;
            list.Label($"Invalid pattern: {Settings.DropRegexLatestError}", tooltip: "The regular expression could not be compiled. Please check the syntax (.NET regular expressions) and try again.");
        }
        list.End();
        _foo++;
        base.DoSettingsWindowContents(canvas);
    }

    public override string SettingsCategory() => "Post Office";

    private static string PascalCaseToDisplayString(string pascalCase)
    {
        StringBuilder bobTheBuilder = new();
        for (int i = 0; i < pascalCase.Length; i++)
        {
            if (i > 0 && char.IsUpper(pascalCase[i]))
            {
                bobTheBuilder.Append(' ');
            }
            bobTheBuilder.Append(pascalCase[i]);
        }
        return bobTheBuilder.ToString();
    }
}
