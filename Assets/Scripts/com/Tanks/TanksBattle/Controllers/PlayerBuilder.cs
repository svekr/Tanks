using com.Tanks.Managers.InputManager;
using com.Tanks.TanksBattle.Controllers.Tank;
using com.Tanks.TanksBattle.Controllers.Tank.Input;
using com.Tanks.TanksBattle.Controllers.Tank.Model;
using com.Tanks.TanksBattle.Controllers.Tank.Movement;
using com.Tanks.TanksBattle.Controllers.Tank.Physics;
using com.Tanks.TanksBattle.Controllers.Tank.View;
using com.Tanks.TanksBattle.Controllers.Updater;
using com.Tanks.TanksBattle.Settings;
using UnityEngine;

namespace com.Tanks.TanksBattle.Controllers {
    public class PlayerBuilder : MonoBehaviour {
        [SerializeField] private TankView _tankOnScene;
        [SerializeField] private PlayerMovementSettings _settings;

        public ITankModel BuildTank(ICommonUpdater updater, InputManager inputManager) {
            // var tankView = Instantiate(_tankPrefab, Vector3.zero, Quaternion.identity, transform);
            var tankView = _tankOnScene;
            tankView.SetExternalUpdater(updater);
            return BuildPlayerTank(tankView, updater, inputManager);
        }

        private ITankModel BuildPlayerTank(ITankView view, ICommonUpdater updater, InputManager inputManager) {
            var settings = _settings;
            var physics = new TankPhysicsUnity(view.Transform);
            var input = new TankInput(inputManager, view, settings.Input);
            var movementArgs = new TankMovementArguments() {
                PhysicsProvider = physics,
                PhysicsUpdater = view,
                MovementInput = input.Movement,
                Settings = settings.Speed
            };
            var movement = GetTankMovement(settings.MovementType, movementArgs);
            return new TankModel(view, physics, input, movement);
        }

        private ATankMovement GetTankMovement(MovementType type, TankMovementArguments args) {
            switch (type) {
                case MovementType.Caterpillar: return new TankMovementCaterpillar(args);
                 default: return new TankMovementSimple(args);
            };
        }
    }
}