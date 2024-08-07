﻿using UnityEngine;
using Utils.Updatable;

namespace com.Tanks.TanksBattle.Tank.Physics {
    public interface ITankPhysics : IUpdatable, IUpdater {
        bool IsActive { get; }
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        Vector3 Velocity { get; set; }
        Vector3 AngularVelocity { get; set; }

        void Move(Vector3 position, Quaternion rotation);
        void Destroy();
    }
}