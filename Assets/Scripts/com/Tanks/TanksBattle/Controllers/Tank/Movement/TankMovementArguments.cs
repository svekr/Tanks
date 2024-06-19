using com.Tanks.TanksBattle.Controllers.Tank.Input;
using com.Tanks.TanksBattle.Controllers.Tank.Physics;
using com.Tanks.TanksBattle.Controllers.Updater;
using com.Tanks.TanksBattle.Settings;

namespace com.Tanks.TanksBattle.Controllers.Tank.Movement {
    public class TankMovementArguments {
        public ITankPhysics PhysicsProvider;
        public IPhysicsUpdater PhysicsUpdater;
        public ITankMovementInput MovementInput;
        public TankSpeedSettings Settings;
    }
}