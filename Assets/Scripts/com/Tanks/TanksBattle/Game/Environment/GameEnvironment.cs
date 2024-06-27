using System;
using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.Environment.Spawn;
using UnityEngine;

namespace com.Tanks.TanksBattle.Game.Environment {
    public class GameEnvironment : MonoBehaviour {
        [SerializeField] private SpawnZoneProvider[] _playerSpawnZones;
        [SerializeField] private SpawnZoneProvider[] _enemiesSpawnZones;

        private SpawnZone[] _spawnZonesPlayer;
        private SpawnZone[] _spawnZonesEnemy;

        public SpawnZone[] SpawnZonesPlayer => _spawnZonesPlayer ??= GetSpawnZones(_playerSpawnZones);
        public SpawnZone[] SpawnZonesEnemy => _spawnZonesEnemy ??= GetSpawnZones(_enemiesSpawnZones);

        private SpawnZone[] GetSpawnZones(IReadOnlyCollection<SpawnZoneProvider> spawnObjects) {
            if (spawnObjects == null || spawnObjects.Count == 0) return null;

            var result = new SpawnZone[spawnObjects.Count];
            var counter = 0;

            foreach (var spawnObject in spawnObjects) {
                if (spawnObject == null) continue;
                var center = spawnObject.transform.position;
                var size = spawnObject.transform.localScale;
                var spawnZone = new SpawnZone(center, size);
                result[counter] = spawnZone;
                counter++;
            }

            if (counter < result.Length) {
                Array.Resize(ref result, counter);
            }
            return result;
        }
    }
}