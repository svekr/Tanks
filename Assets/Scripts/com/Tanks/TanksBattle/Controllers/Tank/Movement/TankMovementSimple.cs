using UnityEngine;

namespace com.Tanks.TanksBattle.Controllers.Tank.Movement {
    public class TankMovementSimple : ATankMovement {
        public TankMovementSimple(TankMovementArguments arguments) : base(arguments) {
        }

        override protected void UpdateMovement(float deltaTime) {
            var modelRotation = _arguments.PhysicsProvider.Rotation;
            var linearSpeed = _arguments.Settings.Linear * deltaTime;
            var angularSpeed = _arguments.Settings.Angular * linearSpeed;
            var axis1 = _arguments.MovementInput.Axis1;
            var axis2 = _arguments.MovementInput.Axis2;

            if (axis1 < 0f) {
                axis2 *= -1;
            }

            _arguments.PhysicsProvider.Position += modelRotation * Vector3.forward * axis1 * linearSpeed;
            _arguments.PhysicsProvider.Rotation = modelRotation * Quaternion.Euler(0f, axis2 * angularSpeed, 0f);
        }
    }
}