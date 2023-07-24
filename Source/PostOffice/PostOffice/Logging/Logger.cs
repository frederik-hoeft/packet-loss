namespace PostOffice.Logging;

internal static class Logger
{
    public static void Log(string message)
    {
        if (PostOfficeMod.Settings.enableLogging)
        {
            Verse.Log.Message($"[{nameof(PostOffice)}] {message}");
        }
    }

    public static void LogVerbose(string message)
    {
        if (PostOfficeMod.Settings.enableVerboseLogging)
        {
            Log(message);
        }
    }

    public static void Error(string message) => 
        Verse.Log.Error($"[{nameof(PostOffice)}] {message}");

    public static void LogAlways(string message) =>
        Verse.Log.Message($"[{nameof(PostOffice)}] {message}");
}
