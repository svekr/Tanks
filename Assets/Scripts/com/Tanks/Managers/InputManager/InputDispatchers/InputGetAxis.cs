using System;
using UnityEngine;

namespace com.Tanks.Managers.InputManager.InputDispatchers {
    public class InputGetAxis : InputDispatcher<string, float, Action<float>> {
        override public float GetValue(string key) {
            return (_simulations.TryGetValue(key, out var value) && value != 0f) ? value : Input.GetAxis(key);
        }

        override protected void AdvanceTime(float deltaTime) {
            foreach (var kvp in _listeners) {
                var value = GetValue(kvp.Key);
                _simulations.Remove(kvp.Key);
                foreach (var listener in kvp.Value) {
                    listener?.Invoke(value);
                }
            }
        }

        override protected bool IsKeyInvalid(string key) {
            return string.IsNullOrEmpty(key);
        }
    }
}