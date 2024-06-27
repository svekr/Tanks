using com.Tanks.TanksBattle.Tank.Movement.Input;
using com.Tanks.TanksBattle.Tank.Physics;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Movement {
    public class TankMovementCaterpillar : TankMovementBase<ITankMovementInputCaterpillar> {
        override public TankMovementType MovementType => TankMovementType.Caterpillar;

        public TankMovementCaterpillar(ITankPhysics model, ITankMovementInputCaterpillar input, ITankVelocitySettings velocity) :
            base(model, input, velocity) {
        }

        override protected void UpdateModel(float deltaTime) {
            var modelRotation = _model.Rotation;
            var linearVelocity = _velocity.Linear * deltaTime;
            var angularVelocity = _velocity.Angular * linearVelocity;
            var axisL = _input.AxisLeft;
            var axisR = _input.AxisRight;

            var forward = (axisL + axisR) * 0.5f;
            forward = Mathf.Lerp(0, forward, forward * forward);
            var deltaRotation = Quaternion.Euler(0f, (axisL - axisR) * angularVelocity, 0f);

            _model.Move(
                _model.Position + modelRotation * Vector3.forward * (forward * linearVelocity),
                modelRotation * deltaRotation
            );
        }
    }
}