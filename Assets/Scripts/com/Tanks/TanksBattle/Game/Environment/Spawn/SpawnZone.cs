using UnityEngine;

namespace com.Tanks.TanksBattle.Game.Environment.Spawn {
    public struct SpawnZone {
        public Vector3 Position => Bounds.center;
        public Bounds Bounds { get; private set; }

        public SpawnZone(Vector3 position, Vector3 size) {
            Bounds = new Bounds(position, size);
        }

        public SpawnZone(Vector3 position, Quaternion rotation, Vector3 size) : this(position, rotation * size) {

        }
    }
}