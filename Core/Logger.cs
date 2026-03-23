namespace StardewUI;

internal static class Logger
{
#if DEBUG
    private const LogLevel DEFAULT_LOG_LEVEL = LogLevel.Debug;
#else
    private const LogLevel DEFAULT_LOG_LEVEL = LogLevel.Trace;
#endif

    internal static IMonitor? Monitor;

    /// <inheritdoc cref="IMonitor.Log(string, LogLevel)"/>
    public static void Log(string message, LogLevel level = DEFAULT_LOG_LEVEL)
    {
        Monitor?.Log(message, level);
    }

    /// <inheritdoc cref="IMonitor.LogOnce(string, LogLevel)"/>
    public static void LogOnce(string message, LogLevel level = DEFAULT_LOG_LEVEL)
    {
        Monitor?.LogOnce(message, level);
    }
}
