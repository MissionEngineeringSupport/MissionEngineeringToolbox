using Serilog;

namespace MissionEngineering.Core;

public class LogUtilities
{
    public static void CreateLogger(string fileName)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(fileName, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }

    public static void CloseLog()
    {
        Log.CloseAndFlush();
    }

    public static void LogInformation(string message, params object?[]? propertyValues)
    {
        Log.Information(message, propertyValues);
    }
}