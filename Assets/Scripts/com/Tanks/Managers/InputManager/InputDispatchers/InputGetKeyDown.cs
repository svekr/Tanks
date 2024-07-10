using UnityEngine;

namespace com.Tanks.Managers.InputManager.InputDispatchers {
    public class InputGetKeyDown : InputGetKey {
        override public bool GetValue(KeyCode key) {
            return Input.GetKeyDown(key);
        }
    }
}