using UnityEngine;

namespace com.Tanks.Managers.InputManager.InputDispatchers {
    public class AInputGetKeyDown : AInputGetKey {
        override public bool GetValue(KeyCode key) {
            return Input.GetKeyDown(key) || _simulations.ContainsKey(key);
        }
    }
}