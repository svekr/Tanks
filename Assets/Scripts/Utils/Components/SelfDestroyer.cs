using System.Collections;
using UnityEngine;
using Utils.Pool;

namespace Utils.Components {
    public class SelfDestroyer : PoolableMonoBehaviour {
        [SerializeField] private float _destroyAfter = 1f;

        private Coroutine _coroutine;
        private WaitForSeconds _wait;

        private void OnEnable() {
            _coroutine = StartCoroutine(DelayedDestroy());
        }

        private void OnDisable() {
            if (_coroutine == null) return;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator DelayedDestroy() {
            _wait ??= new WaitForSeconds(_destroyAfter);
            yield return _wait;
            _coroutine = null;
            ReturnToPool();
        }
    }
}