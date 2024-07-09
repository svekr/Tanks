using System.Collections;
using UnityEngine;
using Utils.Pool;

namespace Utils.Components {
    public class SelfDestroyer : PoolableMonoBehaviour {
        [SerializeField] private float _destroyAfter = 1f;

        private void OnEnable() {
            StartCoroutine(DelayedDestroy(_destroyAfter));
        }

        private IEnumerator DelayedDestroy(float delay) {
            yield return new WaitForSeconds(delay);
            ReturnToPool();
        }
    }
}