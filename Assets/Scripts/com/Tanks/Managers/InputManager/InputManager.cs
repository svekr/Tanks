using System;
using com.Tanks.Managers.InputManager.InputDispatchers;
using com.Tanks.TanksBattle.Controllers.Updater;

namespace com.Tanks.Managers.InputManager {
    public class InputManager : IUpdatable, IUpdater {
        public event Action<float> Updated;

        public AInputGetAxis GetAxis { get; } = new();
        public AInputGetKeyDown GetKeyDown { get; } = new();
        public AInputGetKey GetKey { get; } = new();
        public AInputGetKeyUp GetKeyUp { get; } = new();
        public bool IsEnable { get; set; } = true;

        public void OnUpdate(float deltaTime) {
            if (!IsEnable) return;
            Updated?.Invoke(deltaTime);
            GetAxis.OnUpdate(deltaTime);
            GetKeyDown.OnUpdate(deltaTime);
            GetKey.OnUpdate(deltaTime);
            GetKeyUp.OnUpdate(deltaTime);
        }


    }
}