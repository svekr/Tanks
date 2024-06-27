using UnityEngine;

namespace Utils.Extensions {
    static public class ArrayExtension {
        static public void Shuffle<T>(this T[] array) {
            if (array == null) return;
            var count = array.Length;
            for (var i = 0; i < count; i++) {
                var r = Random.Range(0, count);
                (array[r], array[i]) = (array[i], array[r]);
            }
        }
    }
}