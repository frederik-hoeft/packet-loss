using Verse;

namespace PacketLoss;

public class PacketLossSettings : ModSettings
{
    // general
    internal bool isActive = true;
    internal bool enableLogging = true;
    internal bool enableVerboseLogging = false;

    // rules
    internal bool dropThreatBig = true;
    internal bool dropThreatSmall = true;
    internal bool dropNegativeEvent = true;

    public override void ExposeData()
    {
        Scribe_Values.Look(ref isActive, nameof(isActive), true);
        Scribe_Values.Look(ref enableLogging, nameof(enableLogging), true);
        Scribe_Values.Look(ref enableVerboseLogging, nameof(enableVerboseLogging), true);

        Scribe_Values.Look(ref dropThreatBig, nameof(dropThreatBig), true);
        Scribe_Values.Look(ref dropThreatSmall, nameof(dropThreatSmall), true);
        Scribe_Values.Look(ref dropNegativeEvent, nameof(dropNegativeEvent), true);
    }
}
