using UnityEngine;

namespace com.Tanks.TanksBattle.Controllers.Tank.Movement {
    public class TankMovementCaterpillar : ATankMovement {
        public TankMovementCaterpillar(TankMovementArguments arguments) : base(arguments) {
        }

        override protected void UpdateMovement(float deltaTime) {
            var modelRotation = _arguments.PhysicsProvider.Rotation;
            var linearSpeed = _arguments.Settings.Linear * deltaTime;
            var angularSpeed = _arguments.Settings.Angular * linearSpeed;
            var axis1 = _arguments.MovementInput.Axis1;
            var axis2 = _arguments.MovementInput.Axis2;

            var axis = (axis1 + axis2) * 0.5f;
            axis = Mathf.Lerp(0, axis, axis * axis);
            _arguments.PhysicsProvider.Position += modelRotation * Vector3.forward * axis * linearSpeed;

            var deltaRotation = Quaternion.Euler(0f, (axis1 - axis2) * angularSpeed, 0f);
            _arguments.PhysicsProvider.Rotation = modelRotation * deltaRotation;
        }
    }
}