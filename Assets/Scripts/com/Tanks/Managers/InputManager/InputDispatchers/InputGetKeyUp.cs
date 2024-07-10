using UnityEngine;

namespace com.Tanks.Managers.InputManager.InputDispatchers {
    public class InputGetKeyUp : InputGetKey {
        override public bool GetValue(KeyCode key) {
            return Input.GetKeyUp(key);
        }
    }
}