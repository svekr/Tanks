using com.Tanks.TanksBattle.Tank.Physics;
using Utils.Updatable;

namespace com.Tanks.TanksBattle.Tank.Movement {
    public interface ITankMovement : IUpdatable {
        TankMovementType MovementType { get; }
        bool IsActive { get; set; }

        void SetPhysicsModel(ITankPhysics model);
        void Destroy();
    }
}