using System;
using UnityEngine;

namespace com.Tanks.Managers.InputManager.InputDispatchers {
    public class AInputGetKey : AInputDispatcher<KeyCode, bool, Action> {
        override public bool GetValue(KeyCode key) {
            return Input.GetKey(key) || _simulations.ContainsKey(key);
        }

        override protected void AdvanceTime(float deltaTime) {
            foreach (var kvp in _listeners) {
                var value = GetValue(kvp.Key);
                _simulations.Remove(kvp.Key);
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