using com.Tanks.TanksBattle.Tank.Movement.Input;
using com.Tanks.TanksBattle.Tank.Physics;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Movement {
    public class TankMovementClassic : TankMovementBase<ITankMovementInputClassic> {
        override public TankMovementType MovementType => TankMovementType.Classic;

        public TankMovementClassic(ITankPhysics model, ITankMovementInputClassic input, ITankVelocitySettings velocity) :
            base(model, input, velocity) {
        }

        override protected void UpdateModel(float deltaTime) {
            var modelRotation = _model.Rotation;
            var linearVelocity = _velocity.Linear * deltaTime;
            var angularVelocity = _velocity.Angular * linearVelocity;
            var axisV = _input.AxisVertical;
            var axisH = _input.AxisHorizontal;

            if (axisV < 0f) {
                axisH *= -1;
            }

            _model.Move(
                _model.Position + modelRotation * Vector3.forward * (axisV * linearVelocity),
                modelRotation * Quaternion.Euler(0f, axisH * angularVelocity, 0f)
            );
        }
    }
}