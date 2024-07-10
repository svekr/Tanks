using System;
using UnityEngine;

namespace com.Tanks.Managers.InputManager.InputDispatchers {
    public class InputGetKey : InputDispatcher<KeyCode, bool, Action> {
        override public bool GetValue(KeyCode key) {
            return Input.GetKey(key);
        }

        override protected void AdvanceTime(float deltaTime) {
            foreach (var kvp in _listeners) {
                if (!_simulations.Remove(kvp.Key, out var value)) {
                    value = GetValue(kvp.Key);
                }
                if (!value) continue;
                foreach (var listener in kvp.Value) {
                    listener.Invoke();
                }
            }
        }

        override protected bool IsKeyInvalid(KeyCode key) {
            return key == KeyCode.None;
        }
    }
}