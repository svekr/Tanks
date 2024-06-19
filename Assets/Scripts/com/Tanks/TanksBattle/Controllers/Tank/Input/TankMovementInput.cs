using System;
using com.Tanks.Managers.InputManager;
using com.Tanks.TanksBattle.Controllers.Updater;
using com.Tanks.TanksBattle.Settings;

namespace com.Tanks.TanksBattle.Controllers.Tank.Input {
    public class TankMovementInput : ITankMovementInput {
        private InputManager _inputManager;

        private IUpdater _updateProvider;

        private TankInputSettings _settings;

        public event Action<float, float> OnInput;

        public float Axis1 { get; private set; }
        public float Axis2 { get; private set; }

        public TankMovementInput(InputManager inputManager, IUpdater updateProvider,
            TankInputSettings settings) {
            _inputManager = inputManager;
            _updateProvider = updateProvider;
            _settings = settings;
            _updateProvider.Updated += Updated;
        }

        public void Destroy() {
            _updateProvider.Updated -= Updated;
        }

        private void Updated(float deltaTime) {
            Axis1 = _inputManager.GetAxis.GetValue(_settings.Axis1Name);
            Axis2 = _inputManager.GetAxis.GetValue(_settings.Axis2Name);
            OnInput?.Invoke(Axis1, Axis2);
        }
    }
}