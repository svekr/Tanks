using com.Tanks.TanksBattle.Controllers.Updater;

namespace com.Tanks.TanksBattle.Controllers.Tank.Movement {
    abstract public class ATankMovement : IUpdatable {
        protected TankMovementArguments _arguments;

        public ATankMovement(TankMovementArguments arguments) {
            _arguments = arguments;
            _arguments.PhysicsUpdater.PhysicsUpdated += OnUpdate;
        }

        public void Destroy() {
            _arguments.PhysicsUpdater.PhysicsUpdated -= OnUpdate;
            _arguments = null;
        }

        public void OnUpdate(float deltaTime) {
            if (!_arguments.PhysicsProvider.IsActive) return;
            UpdateMovement(deltaTime);
        }

        abstract protected void UpdateMovement(float deltaTime);
    }
}