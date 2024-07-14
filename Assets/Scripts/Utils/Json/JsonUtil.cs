
namespace Utils.Json {
    static public class JsonUtil {
        static public string Serialize(object value, bool indented = false) {
            return UnityEngine.JsonUtility.ToJson(value, indented);
        }

        static public T Deserialize<T>(string value) {
            return UnityEngine.JsonUtility.FromJson<T>(value);
        }
    }
}