using UnityEngine;

namespace com.Tanks.TanksBattle.Game.Environment.Spawn {
    public class SpawnZoneProvider : MonoBehaviour {
        public SpawnZone GetSpawnZone() {
            return new SpawnZone(transform.position, transform.localScale);
        }
    }
}