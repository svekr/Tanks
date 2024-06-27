using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Physics;
using com.Tanks.TanksBattle.Tank.View;

namespace com.Tanks.TanksBattle.Tank {
    public class TankModel : ITankModel {
        public string Name { get; private set; }
        public ITankView View { get; private set; }
        public ITankPhysics PhysicsModel { get; private set; }
        public ITankMovement Movement { get; private set; }

        public TankModel(string name, ITankView view, ITankPhysics physicsModel, ITankMovement movement) {
            Name = name;
            View = view;
            PhysicsModel = physicsModel;
            Movement = movement;
        }

        public bool DoUpdate(float deltaTime) {
            return PhysicsModel.DoUpdate(deltaTime);
        }

        public void Destroy() {
            View.Destroy();
            Movement.Destroy();
            PhysicsModel.Destroy();
        }
    }
}