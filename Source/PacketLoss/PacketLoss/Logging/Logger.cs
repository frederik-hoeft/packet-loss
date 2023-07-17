namespace PacketLoss.Logging;

internal static class Logger
{
    public static void Log(string message)
    {
        if (PacketLossMod.Settings.enableLogging)
        {
            Verse.Log.Message($"[{nameof(PacketLoss)}] {message}");
        }
    }

    public static void LogVerbose(string message)
    {
        if (PacketLossMod.Settings.enableVerboseLogging)
        {
            Log(message);
        }
    }

    public static void Error(string message) => 
        Verse.Log.Error($"[{nameof(PacketLoss)}] {message}");
}
