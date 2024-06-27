using System;
using com.Tanks.Managers.InputManager.InputDispatchers;
using Utils.Updatable;

namespace com.Tanks.Managers.InputManager {
    public class InputManager : IUpdatable {
        public AInputGetAxis GetAxis { get; } = new();
        public AInputGetKeyDown GetKeyDown { get; } = new();
        public AInputGetKey GetKey { get; } = new();
        public AInputGetKeyUp GetKeyUp { get; } = new();
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