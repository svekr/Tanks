using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.Pool {
    public class GameObjectPool<T> where T: Component, IPoolable {
        private T _prefab;
        private int _capacity;
        private Transform _container;
        private Stack<T> _pool;

        public GameObjectPool(T prefab, int capacity, Transform container = null) {
            if (prefab == null) {
                throw new ArgumentNullException();
            }
            _prefab = prefab;
            _capacity = capacity;
            _container = container;
            _pool = new Stack<T>(_capacity);
        }

        public T Get() {
            if (!_pool.TryPop(out var result)) {
                result = Object.Instantiate(_prefab);
            }
            result.SetReturnToPoolAction(ReturnToPool);
            result.gameObject.SetActive(true);
            return result;
        }

        private void ReturnToPool(IPoolable poolable) {
            if (poolable == null) return;
            var releasedObject = (T)poolable;
            if (_pool.Contains(releasedObject)) return;
            if (_pool.Count >= _capacity) {
                Object.Destroy(releasedObject.GameObject);
                return;
            }
            releasedObject.GameObject.SetActive(false);
            if (_container != null) {
                releasedObject.GameObject.transform.SetParent(_container);
            }
            _pool.Push(releasedObject);
        }
    }
}