using System;
using UnityEngine;

namespace Utils.Pool {
    public class PoolableMonoBehaviour : MonoBehaviour, IPoolable {
        private Action<IPoolable> _returnToPoolAction;

        GameObject IPoolable.GameObject => gameObject;

        public void ReturnToPool() {
            if (_returnToPoolAction != null) {
                _returnToPoolAction?.Invoke(this);
                return;
            }
            Destroy(gameObject);
        }

        void IPoolable.SetReturnToPoolAction(Action<IPoolable> returnToPoolAction) {
            _returnToPoolAction = returnToPoolAction;
        }
    }
}