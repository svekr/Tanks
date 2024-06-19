namespace com.Tanks.Managers.Logger {
    public class UnityLogger: ILogger {
        public void Log(object message) {
            UnityEngine.Debug.Log(message);
        }

        public void LogWarning(object message) {
            UnityEngine.Debug.LogWarning(message);
        }

        public void LogError(object message) {
            UnityEngine.Debug.LogError(message);
        }
    }
}