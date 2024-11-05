

using Microsoft.Extensions.Logging;


namespace ComputerStore.Utils;

public static class AppLogger
{
    private static ILoggerFactory? _loggerFactory;

    public static void ConfigureLogger(LogLevel minimumLevel = LogLevel.Information)
    {
        _loggerFactory = LoggerFactory.Create(builder => {
            builder
                .AddConsole()
                .SetMinimumLevel(minimumLevel);
        });
    }


    public static ILogger<T> GetLogger<T>()
    {
        if(_loggerFactory == null)
        {
            ConfigureLogger();
        }

        return _loggerFactory?.CreateLogger<T>() ?? throw new InvalidOperationException("LoggerFactory is not configured.");
    }
}