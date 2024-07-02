using com.Tanks.TanksBattle.Tank.Movement.Input;
using com.Tanks.TanksBattle.Tank.Physics;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Movement {
    public class TankMovementClassic : TankMovementBase<ITankMovementInputClassic> {
        private Vector3 _angularVelocity = Vector3.up;
        override public TankMovementType MovementType => TankMovementType.Classic;

        public TankMovementClassic(ITankPhysics model, ITankMovementInputClassic input, ITankVelocitySettings velocity) :
            base(model, input, velocity) {
        }

        override protected void UpdateModel(float deltaTime) {
            var axisV = _input.AxisVertical;
            var axisH = (axisV < 0f) ? -_input.AxisHorizontal : _input.AxisHorizontal;

            _model.Velocity = _model.Rotation * Vector3.forward * (axisV * _velocity.Linear);
            _angularVelocity.y = axisH * _velocity.Angular;
            _model.AngularVelocity = _angularVelocity;
        }
    }
}