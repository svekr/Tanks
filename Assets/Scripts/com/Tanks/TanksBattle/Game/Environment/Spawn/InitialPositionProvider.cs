using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using UnityEngine;
using Utils.Extensions;

namespace com.Tanks.TanksBattle.Game.Environment.Spawn {
    public class InitialPositionProvider : IInitialPositionProvider {
        private const float MIN_DISTANCE = 2f;

        private readonly ILogger _logger;

        private IGameEnvironment _environment;
        private IReadOnlyList<IGameEntity> _entities;

        public InitialPositionProvider(ILogger logger) {
            _logger = logger;
        }

        public void SetEnvironment(IGameEnvironment environment, IReadOnlyList<IGameEntity> existingEntities) {
            _environment = environment;
            _entities = existingEntities;
        }

        public void SetEntityToInitialPosition(IGameEntity entity) {
            if (entity == null) return;
            var logMsgHead = $"{GetType().Name}.{nameof(SetEntityToInitialPosition)}(type:{entity.Type}, name:{entity.Name}):";
            if (TryGetEntityInitialPosition(entity.Type, entity.Name, out var position, out var rotation)) {
                _logger?.Log($"{logMsgHead} Set position={position}");
                entity.SetPosition(position, rotation);
            } else {
                _logger?.LogWarning($"{logMsgHead} Can not find suitable position");
            }
        }

        public bool TryGetEntityInitialPosition(EntityType entityType, string entityName, out Vector3 position, out Quaternion rotation) {
            position = Vector3.zero;
            rotation = Quaternion.identity;
            var spawnZones = _environment?.GetSpawnZones(entityType);
            if (spawnZones == null) {
                var logMsgHead = $"{GetType().Name}.{nameof(TryGetEntityInitialPosition)}(type:{entityType}, name:{entityName}):";
                _logger?.LogWarning($"{logMsgHead} No spawn zones for this type");
                return false;
            }

            var zoneIndexes = GetRandomIndexes(spawnZones.Length);
            foreach (var zoneIndex in zoneIndexes) {
                var zone = spawnZones[zoneIndex];
                if (!TryGetPosition(zone, out position)) continue;
                rotation = Quaternion.LookRotation(Vector3.zero - position);
                return true;
            }

            return false;
        }

        private int[] GetRandomIndexes(int count) {
            var result = new int[count];
            for (var i = 0; i < count; i++) {
                result[i] = i;
            }
            result.Shuffle();
            return result;
        }

        private bool TryGetPosition(SpawnZone zone, out Vector3 position) {
            var size = (zone.Bounds.size + Vector3.one) / MIN_DISTANCE;
            var triesCount = Mathf.CeilToInt(size.x * size.y * size.z);
            while (triesCount > 0) {
                position = zone.Bounds.GetRandomPoint();
                if (IsPositionValid(position)) return true;
                triesCount--;
            }
            position = Vector3.zero;
            return false;
        }

        private bool IsPositionValid(Vector3 position) {
            foreach (var entity in _entities) {
                if ((entity.Position - position).magnitude < MIN_DISTANCE) return false;
            }
            return true;
        }
    }
}