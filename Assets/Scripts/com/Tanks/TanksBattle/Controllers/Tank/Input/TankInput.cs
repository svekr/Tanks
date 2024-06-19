using com.Tanks.Managers.InputManager;
using com.Tanks.TanksBattle.Controllers.Updater;
using com.Tanks.TanksBattle.Settings;

namespace com.Tanks.TanksBattle.Controllers.Tank.Input {
    public class TankInput : ITankInput {
        public ITankMovementInput Movement { get; }
        public ITankFireInput Fire { get; }

        public TankInput(InputManager inputManager, IUpdater updateProvider, TankInputSettings settings) {
            Movement = new TankMovementInput(inputManager, updateProvider, settings);
        }

        public void Destroy() {
            Movement.Destroy();
            Fire.Destroy();
        }
    }
}