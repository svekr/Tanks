using UnityEngine;

namespace com.Tanks.TanksBattle.Game.Environment.Spawn {
    public struct SpawnZone {
        public Vector3 Position { get; private set; }
        public Vector3 Size { get; private set; }

        public SpawnZone(Vector3 position, Vector3 size) {
            Position = position;
            Size = size;
        }
    }
}