using Microsoft.Extensions.Logging;

namespace ErrorLogging;

public sealed class LoggingManager {
    private static readonly Lazy<LoggingManager> _instance = new(() => new LoggingManager(new FileLogger()));
    private readonly FileLogger _fileLogger;

    private LoggingManager (FileLogger fileLogger){
        _fileLogger = fileLogger ?? throw new ArgumentNullException(nameof(fileLogger));
    }

    public static LoggingManager Instance => _instance.Value;

    public void LogInformation (string message){
        _fileLogger.Log(LogLevel.Information, message, null, (state, exception) => $"Message: {state}");
    }

    public void LogError (Exception ex, string message){
        _fileLogger.Log(LogLevel.Error, message, ex, (state, exception) => $"{state}: {exception?.Message}\nStackTrace: {exception?.StackTrace}");
    }

    public void LogWarning (string message){
        _fileLogger.Log(LogLevel.Warning, message, null, (state, exception) => $"Warning: {state}");
    }

    public void LogWarningWithException (Exception ex, string message){
        _fileLogger.Log(LogLevel.Warning, message, ex, (state, exception) => $"Warning: {state}. Exception: {exception?.Message}");
    }
}