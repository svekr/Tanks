using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Physics;
using com.Tanks.TanksBattle.Tank.View;
using Utils.Updatable;

namespace com.Tanks.TanksBattle.Tank {
    public interface ITankModel : IUpdatable {
        string Name { get; }
        ITankView View { get; }
        ITankPhysics PhysicsModel { get; }
        ITankMovement Movement { get; }

        void Destroy();
    }

    public interface ITankShooting {
        void Destroy();


        public interface ITankShootingSettings {
            bool CanShoot { get; }
            int Damage { get; }
        }
    }

    public interface ITankCollider {

    }
}