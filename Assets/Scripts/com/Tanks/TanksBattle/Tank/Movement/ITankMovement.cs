using UnityEngine;
using Utils.Updatable;

namespace com.Tanks.TanksBattle.Tank.Movement {
    public interface ITankMovement : IUpdatable {
        TankMovementType MovementType { get; }
        bool IsActive { get; set; }

        void Move(Vector3 position, Quaternion rotation);
        void Destroy();
    }
}