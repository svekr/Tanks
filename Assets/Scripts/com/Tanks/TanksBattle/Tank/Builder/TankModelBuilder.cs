using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Physics;
using com.Tanks.TanksBattle.Tank.Settings;
using com.Tanks.TanksBattle.Tank.View;

namespace com.Tanks.TanksBattle.Tank.Builder {
    abstract public class TankModelBuilder : ITankModelBuilder {
        abstract public EntityType EntityType { get; }

        protected List<IGameEntity> Entities { get; }

        protected TankModelBuilder(List<IGameEntity> entities) {
            Entities = entities;
        }

        public ITankModel BuildTank(string name, ITankView view, ITankSettings settings) {
            return new TankModel(EntityType, name, view, settings, this);
        }

        public ITankPhysics BuildPhysics(ITankModel tank) {
            return new TankPhysicsUnityRigidbody(tank.View.Transform);
        }

        abstract public ITankMovement BuildMovement(ITankPhysics physics, ITankMovementSettings settings,
            ITankEventProvider eventProvider);
    }
}