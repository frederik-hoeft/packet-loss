using Verse;

namespace PostOffice;

public class PostOfficeSettings : ModSettings
{
    // general
    internal bool isActive = true;
    internal bool enableMessageSupport = true;
    internal bool enableLogging = true;
    internal bool enableVerboseLogging = false;

    // rules
    internal bool dropThreatBig = true;
    internal bool dropThreatSmall = true;
    internal bool dropNegativeEvent = true;
    internal bool dropAcceptJoiner = false;
    internal bool dropAcceptVisitors = false;
    internal bool dropBabyToChild = false;
    internal bool dropBabyBirth = false;
    internal bool dropBetrayVisitors = false;
    internal bool dropBossgroup = false;
    internal bool dropBundleLetter = false;
    internal bool dropChildBirthday = false;
    internal bool dropChildToAdult = false;
    internal bool dropDeath = false;
    internal bool dropChoosePawn = false;
    internal bool dropNeutralEvent = false;
    internal bool dropNewQuest = false;
    internal bool dropPositiveEvent = false;
    internal bool dropRelicHuntInstallationFound = false;
    internal bool dropRitualOutcomeNegative = false;
    internal bool dropRitualOutcomePositive = false;

    public override void ExposeData()
    {
        Scribe_Values.Look(ref isActive, nameof(isActive), true);
        Scribe_Values.Look(ref enableMessageSupport, nameof(enableMessageSupport), true);
        Scribe_Values.Look(ref enableLogging, nameof(enableLogging), true);
        Scribe_Values.Look(ref enableVerboseLogging, nameof(enableVerboseLogging), true);

        Scribe_Values.Look(ref dropThreatBig, nameof(dropThreatBig), defaultValue: true);
        Scribe_Values.Look(ref dropThreatSmall, nameof(dropThreatSmall), defaultValue: true);
        Scribe_Values.Look(ref dropNegativeEvent, nameof(dropNegativeEvent), defaultValue: true);
        Scribe_Values.Look(ref dropAcceptJoiner, nameof(dropAcceptJoiner), defaultValue: false);
        Scribe_Values.Look(ref dropAcceptVisitors, nameof(dropAcceptVisitors), defaultValue: false);
        Scribe_Values.Look(ref dropBabyToChild, nameof(dropBabyToChild), defaultValue: false);
        Scribe_Values.Look(ref dropBabyBirth, nameof(dropBabyBirth), defaultValue: false);
        Scribe_Values.Look(ref dropBetrayVisitors, nameof(dropBetrayVisitors), defaultValue: false);
        Scribe_Values.Look(ref dropBossgroup, nameof(dropBossgroup), defaultValue: false);
        Scribe_Values.Look(ref dropBundleLetter, nameof(dropBundleLetter), defaultValue: false);
        Scribe_Values.Look(ref dropChildBirthday, nameof(dropChildBirthday), defaultValue: false);
        Scribe_Values.Look(ref dropChildToAdult, nameof(dropChildToAdult), defaultValue: false);
        Scribe_Values.Look(ref dropDeath, nameof(dropDeath), defaultValue: false);
        Scribe_Values.Look(ref dropChoosePawn, nameof(dropChoosePawn), defaultValue: false);
        Scribe_Values.Look(ref dropNeutralEvent, nameof(dropNeutralEvent), defaultValue: false);
        Scribe_Values.Look(ref dropNewQuest, nameof(dropNewQuest), defaultValue: false);
        Scribe_Values.Look(ref dropPositiveEvent, nameof(dropPositiveEvent), defaultValue: false);
        Scribe_Values.Look(ref dropRelicHuntInstallationFound, nameof(dropRelicHuntInstallationFound), defaultValue: false);
        Scribe_Values.Look(ref dropRitualOutcomeNegative, nameof(dropRitualOutcomeNegative), defaultValue: false);
        Scribe_Values.Look(ref dropRitualOutcomePositive, nameof(dropRitualOutcomePositive), defaultValue: false);
    }
}
