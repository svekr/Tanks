using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Movement.Input;
using com.Tanks.TanksBattle.Tank.Physics;

namespace com.Tanks.TanksBattle.Tank.Builder {
    public class PlayerTankModelBuilder : TankModelBuilder {
        override public EntityType EntityType => EntityType.Player;

        public PlayerTankModelBuilder(List<IGameEntity> entities) : base(entities) {

        }

        override public ITankMovement BuildMovement(ITankPhysics model, ITankMovementSettings settings, ITankEventProvider eventProvider) {
            switch (settings.MovementType) {
                case TankMovementType.Classic:
                    return new TankMovementClassic(model, new TankMovementInputClassic(), settings.Velocity);
                case TankMovementType.Caterpillar:
                    return new TankMovementCaterpillar(model, new TankMovementInputCaterpillar(), settings.Velocity);
                default:
                    return new TankMovementNone(model);
            }
        }
    }
}