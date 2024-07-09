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
    public class EnemyTankModelBuilder : TankModelBuilder {
        override public EntityType EntityType => EntityType.Enemy;

        public EnemyTankModelBuilder(List<IGameEntity> entities, GameContext context) : base(entities, context) {

        }

        override public ITankMovement BuildMovement(ITankPhysics model, ITankMovementSettings settings,
            ITankEventProvider eventProvider) {
            switch (settings.MovementType) {
                case TankMovementType.Classic:
                    var aiClassic = new TankMovementAIClassic(GameContext.AISettings.Movement, model, eventProvider, Entities);
                    return new TankMovementClassic(model, aiClassic, settings.Velocity);
                case TankMovementType.Caterpillar:
                    var aiCaterpillar = new TankMovementAICaterpillar(GameContext.AISettings.Movement, model, eventProvider, Entities);
                    return new TankMovementCaterpillar(model, aiCaterpillar, settings.Velocity);
                default:
                    return new TankMovementNone(model);
            }
        }

        override public ITankShooting BuildShooting(ITankModel tank, ITankShootingSettings settings) {
            var input = new ShootingInputAI(GameContext.AISettings.Shooting, settings.ReloadDuration, tank, Entities);
            return new TankShooting(input, GameContext, tank);
        }
    }
}