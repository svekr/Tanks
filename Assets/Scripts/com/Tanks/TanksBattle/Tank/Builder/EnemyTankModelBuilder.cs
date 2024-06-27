using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Movement.Input;
using com.Tanks.TanksBattle.Tank.Physics;
using com.Tanks.TanksBattle.Tank.Settings;
using com.Tanks.TanksBattle.Tank.View;

namespace com.Tanks.TanksBattle.Tank.Builder {
    public class EnemyTankModelBuilder : ITankModelBuilder {
        public ITankModel BuildTank(string name, ITankView view, ITankSettings settings) {
            var physicsModel = new TankPhysicsUnityRigidbody(view.Transform);
            var movement = GetMovement(physicsModel, settings.Movement);
            return new TankModel(name, view, physicsModel, movement);
        }

        private ITankMovement GetMovement(ITankPhysics model, ITankMovementSettings settings) {
            switch (settings.MovementType) {
                case TankMovementType.Classic:
                    return new TankMovementClassic(model, null, settings.Velocity);
                case TankMovementType.Caterpillar:
                    return new TankMovementCaterpillar(model, null, settings.Velocity);
                default:
                    return new TankMovementNone(model);
            }
        }
    }
}