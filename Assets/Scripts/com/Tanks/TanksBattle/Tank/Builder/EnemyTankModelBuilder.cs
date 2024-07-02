using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Physics;

namespace com.Tanks.TanksBattle.Tank.Builder {
    public class EnemyTankModelBuilder : TankModelBuilder {
        override public EntityType EntityType => EntityType.Enemy;

        public EnemyTankModelBuilder(List<IGameEntity> entities) : base(entities) {

        }

        override public ITankMovement BuildMovement(ITankPhysics model, ITankMovementSettings settings,
            ITankEventProvider eventProvider) {
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