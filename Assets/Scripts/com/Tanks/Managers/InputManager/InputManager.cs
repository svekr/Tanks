using System;
using com.Tanks.Managers.InputManager.InputDispatchers;
using Utils.Updatable;

namespace com.Tanks.Managers.InputManager {
    public class InputManager : IUpdatable {
        public InputGetAxis GetAxis { get; } = new();
        public InputGetKeyDown GetKeyDown { get; } = new();
        public InputGetKey GetKey { get; } = new();
        public InputGetKeyUp GetKeyUp { get; } = new();
        public bool IsEnable { get; set; } = true;

        public bool DoUpdate(float deltaTime) {
            if (!IsEnable) return true;
            GetAxis.DoUpdate(deltaTime);
            GetKeyDown.DoUpdate(deltaTime);
            GetKey.DoUpdate(deltaTime);
            GetKeyUp.DoUpdate(deltaTime);
            return true;
        }
    }
}