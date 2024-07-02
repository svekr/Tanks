using com.Tanks.TanksBattle.Tank.Movement.Input;
using com.Tanks.TanksBattle.Tank.Physics;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Movement {
    public class TankMovementCaterpillar : TankMovementBase<ITankMovementInputCaterpillar> {
        private Vector3 _angularVelocity = Vector3.up;

        override public TankMovementType MovementType => TankMovementType.Caterpillar;

        public TankMovementCaterpillar(ITankPhysics model, ITankMovementInputCaterpillar input, ITankVelocitySettings velocity) :
            base(model, input, velocity) {
        }

        override protected void UpdateModel(float deltaTime) {
            var axisL = _input.AxisLeft;
            var axisR = _input.AxisRight;

            var forward = (axisL + axisR) * 0.5f;
            forward = Mathf.Lerp(0, forward, forward * forward);

            _model.Velocity = _model.Rotation * Vector3.forward * (forward * _velocity.Linear);
            _angularVelocity.y = (axisL - axisR) * _velocity.Angular;
            _model.AngularVelocity = _angularVelocity;
        }
    }
}