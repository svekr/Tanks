using com.Tanks.TanksBattle.Controllers.Tank.Input;
using com.Tanks.TanksBattle.Controllers.Tank.Movement;
using com.Tanks.TanksBattle.Controllers.Tank.Physics;
using com.Tanks.TanksBattle.Controllers.Tank.View;

namespace com.Tanks.TanksBattle.Controllers.Tank.Model {
    public class TankModel : ITankModel {
        public ITankView View { get; }
        public ITankPhysics Physics { get; }
        public ITankInput Input { get; }
        public ATankMovement Movement { get; }

        public TankModel(ITankView view, ITankPhysics physics, ITankInput input, ATankMovement movement) {
            View = view;
            Physics = physics;
            Input = input;
            Movement = movement;
        }

        public void Destroy() {
            Movement.Destroy();
            Input.Destroy();
            Physics.Destroy();
            View.Destroy();
        }
    }
}