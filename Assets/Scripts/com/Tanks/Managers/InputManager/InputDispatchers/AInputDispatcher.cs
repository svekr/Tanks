using System;
using System.Collections.Generic;

namespace com.Tanks.Managers.InputManager.InputDispatchers {
    abstract public class AInputDispatcher<TKey, TValue, TListener> where TListener : Delegate {
        protected readonly Dictionary<TKey, List<TListener>> _listeners = new();
        protected readonly Dictionary<TKey, TValue> _simulations = new();

        private readonly Queue<Action> _waiting = new();

        private bool _isLocked = false;

        public void OnUpdate(float deltaTime) {
            _isLocked = true;
            AdvanceTime(deltaTime);
            _isLocked = false;
            while (_waiting.TryDequeue(out var waiting)) {
                waiting.Invoke();
            }
        }

        public Action AddListener(TKey key, TListener listener) {
            if (_isLocked) {
                _waiting.Enqueue(() => AddListener(key, listener));
            } else {
                if (IsKeyInvalid(key) || listener == null) return null;
                if (!_listeners.TryGetValue(key, out var listeners)) {
                    _listeners.Add(key, new List<TListener> { listener });
                } else if (!listeners.Contains(listener)) {
                    listeners.Add(listener);
                }
            }
            return () => RemoveListener(key, listener);
        }

        public void RemoveListener(TKey key, TListener listener) {
            if (_isLocked) {
                _waiting.Enqueue(() => RemoveListener(key, listener));
            } else {
                if (IsKeyInvalid(key) || listener == null) return;
                if (!_listeners.TryGetValue(key, out var listeners)) return;
                listeners.Remove(listener);
                if (listeners.Count == 0) _listeners.Remove(key);
            }
        }

        public void Clear() {
            foreach (var listeners in _listeners.Values) {
                listeners.Clear();
            }
            _listeners.Clear();
            _simulations.Clear();
            _waiting.Clear();
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