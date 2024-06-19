using UnityEngine;

namespace com.Tanks.TanksBattle.Controllers.Tank.Physics {
    public interface ITankPhysics {
        bool IsActive { get; }
        Transform Transform { get; }
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }

        void Destroy();
    }
}