using System;
using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.Environment.Spawn;
using com.Tanks.TanksBattle.Game.GameEntity;
using UnityEngine;

namespace com.Tanks.TanksBattle.Game.Environment {
    public class GameEnvironment : MonoBehaviour, IGameEnvironment {
        [SerializeField] private SpawnZoneProvider[] _playerSpawnZones;
        [SerializeField] private SpawnZoneProvider[] _enemiesSpawnZones;

        private SpawnZone[] _spawnZonesPlayer;
        private SpawnZone[] _spawnZonesEnemy;

        public SpawnZone[] GetSpawnZones(EntityType entityType) {
            switch (entityType) {
                case EntityType.Player:
                    return _spawnZonesPlayer ??= GetSpawnZones(_playerSpawnZones);
                case EntityType.Enemy:
                    return _spawnZonesEnemy ??= GetSpawnZones(_enemiesSpawnZones);
                default:
                    return null;
            }
        }

        private SpawnZone[] GetSpawnZones(IReadOnlyCollection<SpawnZoneProvider> spawnObjects) {
            if (spawnObjects == null || spawnObjects.Count == 0) return null;

            var result = new SpawnZone[spawnObjects.Count];
            var counter = 0;

            foreach (var spawnObject in spawnObjects) {
                if (spawnObject == null) continue;
                result[counter] = spawnObject.GetSpawnZone();
                counter++;
            }

            if (counter < result.Length) {
                Array.Resize(ref result, counter);
            }
            return result;
        }
    }
}