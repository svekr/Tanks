using com.Tanks.TanksBattle.Tank.Movement.Input;
using com.Tanks.TanksBattle.Tank.Physics;

namespace com.Tanks.TanksBattle.Tank.Movement {
    public class TankMovementNone : TankMovementBase<ITankMovementInput> {
        override public TankMovementType MovementType => TankMovementType.Immovable;

        public TankMovementNone(ITankPhysics model) : base(model, null, null) {
        }

        override protected void UpdateModel(float deltaTime) {
        }
    }
}