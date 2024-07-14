using System.Collections.Generic;
using com.Tanks.TanksBattle.Game;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Contacts;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Physics;
using com.Tanks.TanksBattle.Tank.Settings;
using com.Tanks.TanksBattle.Tank.Shooting;
using com.Tanks.TanksBattle.Tank.View;

namespace com.Tanks.TanksBattle.Tank.Builder {
    abstract public class TankModelBuilder : ITankModelBuilder {
        abstract public EntityType EntityType { get; }

        protected IReadOnlyList<IGameEntity> Entities { get; }
        protected GameContext GameContext { get; }

        protected TankModelBuilder(IReadOnlyList<IGameEntity> entities, GameContext context) {
            Entities = entities;
            GameContext = context;
        }

        public ITankModel BuildTank(string name, ITankView view, ITankSettings settings) {
            return new TankModel(EntityType, name, view, settings, this);
        }

        public ITankPhysics BuildPhysics(ITankModel tank) {
            return new TankPhysicsUnityRigidbody(tank.View.Transform);
        }

        public ITankContactor BuildContactor(ITankModel tank) {
            return new TankContactorUnity(tank.View.Transform, tank.EventProvider, Entities);
        }

        abstract public ITankMovement BuildMovement(ITankPhysics physics, ITankMovementSettings settings, ITankEventProvider eventProvider);

        virtual public ITankShooting BuildShooting(ITankModel tank, ITankShootingSettings settings) {
            return null;
        }
    }
}