using System.Collections.Generic;
using com.Tanks.TanksBattle.Game;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Movement.Input;
using com.Tanks.TanksBattle.Tank.Physics;
using com.Tanks.TanksBattle.Tank.Shooting;
using com.Tanks.TanksBattle.Tank.Shooting.Input;

namespace com.Tanks.TanksBattle.Tank.Builder {
    public class PlayerTankModelBuilder : TankModelBuilder {
        override public EntityType EntityType => EntityType.Player;

        public PlayerTankModelBuilder(List<IGameEntity> entities, GameContext context) : base(entities, context) {

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

        override public ITankShooting BuildShooting(ITankModel tank, ITankShootingSettings settings) {
            return new TankShooting(new ShootingInputPlayer(settings.ReloadDuration), GameContext, tank);
        }
    }
}