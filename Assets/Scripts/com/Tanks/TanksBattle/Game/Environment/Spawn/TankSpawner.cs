using System.Collections.Generic;
using com.Tanks.TanksBattle.Tank;
using UnityEngine;
using Utils.Extensions;

namespace com.Tanks.TanksBattle.Game.Environment.Spawn {
    public class TankSpawner {
        private readonly ILogger _logger;

        public TankSpawner(ILogger logger) {
            _logger = logger;
        }
        public void SpawnTank(ITankModel tank, SpawnZone[] spawnZones, List<ITankModel> tanks, float minDistance = 2f) {
            var zoneIndexes = GetRandomIndexes(spawnZones.Length);
            foreach (var zoneIndex in zoneIndexes) {
                var zone = spawnZones[zoneIndexes[zoneIndex]];
                if (IsPositionValid(zone.Position, tanks, minDistance)) {
                    SpawnTank(tank, zone.Position);
                    return;
                }
            }
            _logger?.LogWarning($"Can not spawn {tank.Name}");
        }

        private int[] GetRandomIndexes(int count) {
            var result = new int[count];
            for (var i = 0; i < count; i++) {
                result[i] = i;
            }
            result.Shuffle();
            return result;
        }

        private bool IsPositionValid(Vector3 position, List<ITankModel> tanks, float minDistance) {
            foreach (var tank in tanks) {
                if ((tank.PhysicsModel.Position - position).magnitude < minDistance) return false;
            }
            return true;
        }

        private void SpawnTank(ITankModel tank, Vector3 position) {
            var rotation = Quaternion.LookRotation(Vector3.zero - position);
            tank.PhysicsModel.Move(position, rotation);
            _logger.Log($"Spawn {tank.Name} at {position}");
        }
    }
}