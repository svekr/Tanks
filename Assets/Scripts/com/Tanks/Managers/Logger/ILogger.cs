﻿public interface ILogger {
    void Log(object message);

    void LogWarning(object message);

    void LogError(object message);
}