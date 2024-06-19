using UnityEngine;

namespace com.Tanks.Managers.InputManager.InputDispatchers {
    public class AInputGetKeyUp : AInputGetKey {
        override public bool GetValue(KeyCode key) {
            return Input.GetKeyUp(key) || _simulations.ContainsKey(key);
        }
    }
}