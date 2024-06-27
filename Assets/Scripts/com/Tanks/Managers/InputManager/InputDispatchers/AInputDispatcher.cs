using System;
using System.Collections.Generic;

namespace com.Tanks.Managers.InputManager.InputDispatchers {
    abstract public class AInputDispatcher<TKey, TValue, TListener> where TListener : Delegate {
        protected readonly Dictionary<TKey, List<TListener>> _listeners = new();
        protected readonly Dictionary<TKey, TValue> _simulations = new();

        private readonly Queue<KeyValuePair<TKey, TListener>> _queueToAdd = new();
        private readonly Queue<KeyValuePair<TKey, TListener>> _queueToRemove = new();

        private bool _isLocked;

        public bool DoUpdate(float deltaTime) {
            _isLocked = true;
            AdvanceTime(deltaTime);
            _isLocked = false;

            while (_queueToRemove.TryDequeue(out var queued)) {
                RemoveListener(queued.Key, queued.Value);
            }

            while (_queueToAdd.TryDequeue(out var queued)) {
                AddListener(queued.Key, queued.Value);
            }

            return true;
        }

        public void AddListener(TKey key, TListener listener) {
            if (IsKeyInvalid(key) || listener == null) return;

            if (_isLocked) {
                _queueToAdd.Enqueue(new KeyValuePair<TKey, TListener>(key, listener));
                return;
            }

            if (!_listeners.TryGetValue(key, out var listeners)) {
                _listeners.Add(key, new List<TListener> { listener });
            } else if (!listeners.Contains(listener)) {
                listeners.Add(listener);
            }
        }

        public void RemoveListener(TKey key, TListener listener) {
            if (IsKeyInvalid(key) || listener == null) return;

            if (_isLocked) {
                _queueToRemove.Enqueue(new KeyValuePair<TKey, TListener>(key, listener));
                return;
            }

            if (!_listeners.TryGetValue(key, out var listeners)) return;
            listeners.Remove(listener);
            if (listeners.Count == 0) _listeners.Remove(key);
        }

        public void Clear() {
            foreach (var listeners in _listeners.Values) {
                listeners.Clear();
            }
            _listeners.Clear();
            _simulations.Clear();
            _queueToAdd.Clear();
            _queueToRemove.Clear();
        }

        public void Simulate(TKey key, TValue value) {
            if (!_listeners.ContainsKey(key)) return;
            _simulations[key] = value;
        }

        abstract public TValue GetValue(TKey key);

        abstract protected void AdvanceTime(float deltaTime);

        abstract protected bool IsKeyInvalid(TKey key);
    }
}