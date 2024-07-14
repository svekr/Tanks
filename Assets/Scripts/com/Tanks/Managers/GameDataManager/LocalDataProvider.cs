using System;
using System.IO;
using UnityEngine;
using Utils.Json;

namespace com.Tanks.Managers.GameDataManager {
    public class LocalDataProvider : IDataProvider {
        private const string JSON_EXTENSION = ".json";

        private readonly ILogger _logger;
        public LocalDataProvider(ILogger logger) {
            _logger = logger;
        }

        public void LoadData<T>(Action<T> onLoadComplete, string path) {
            var data = Load<T>(path);
            onLoadComplete?.Invoke(data);
        }

        public void SaveData<T>(T data, string path) {
            var filePath = GetFilePath<T>(path);
            var jsonData = JsonUtil.Serialize(data, true);
            var byteData = System.Text.Encoding.ASCII.GetBytes(jsonData);
            var directoryName = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryName) && directoryName != null) {
                Directory.CreateDirectory(directoryName);
            }
            try {
                File.WriteAllBytes(filePath, byteData);
                _logger.Log("Save data to: " + filePath);
            } catch (Exception e) {
                _logger.LogError("Failed to save data to: " + filePath);
                _logger.LogError("Error " + e.Message);
            }
        }

        public T Load<T>(string path) {
            var filePath = GetFilePath<T>(path);
            if (!Directory.Exists(Path.GetDirectoryName(filePath))) {
                _logger.LogWarning("File or path does not exist! " + filePath);
                return default;
            }
            byte[] jsonDataAsBytes;
            try {
                jsonDataAsBytes = File.ReadAllBytes(filePath);
                _logger.Log("Loaded all data from: " + filePath);
            } catch (Exception e) {
                _logger.LogWarning($"Failed to load data from: {filePath}. Cause: {e.Message}");
                return default;
            }
            var jsonData = System.Text.Encoding.ASCII.GetString(jsonDataAsBytes);
            var returnedData = JsonUtil.Deserialize<T>(jsonData);
            return (T)Convert.ChangeType(returnedData, typeof(T));
        }

        private string GetFilePath<T>(string path) {
            string filePath;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
      filePath = Path.Combine(Application.streamingAssetsPath, ("data/"));
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            filePath = Path.Combine(Application.persistentDataPath, "data/");
#elif UNITY_ANDROID
      filePath = Path.Combine(Application.persistentDataPath, ("data/"));
#elif UNITY_IOS
      filePath = Path.Combine(Application.persistentDataPath, ("data/"));
#endif
            if (string.IsNullOrEmpty(path)) {
                path = typeof(T).Name;
            }
            filePath = Path.Combine(filePath, path + JSON_EXTENSION);
            return filePath;
        }
    }
}