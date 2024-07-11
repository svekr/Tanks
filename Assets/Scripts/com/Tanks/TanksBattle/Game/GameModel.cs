using System;
using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.Environment;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Game.GameEntity.Factory;
using com.Tanks.TanksBattle.Tank;

namespace com.Tanks.TanksBattle.Game {
    public class GameModel {
        public event Action<IGameEntity> OnEntityRemoved;

        private readonly ILogger _logger;
        private readonly List<IGameEntity> _entities;
        private readonly GameEntitiesFactory _entitiesFactory;

        public ITankModel Player { get; private set; }

        public GameModel(ILogger logger, GameContext gameContext, IGameEnvironment environment) {
            _logger = logger;
            _entities = new List<IGameEntity>();
            _entitiesFactory = new GameEntitiesFactory(logger);
            _entitiesFactory.Initialize(gameContext, environment, _entities);
        }

        public void DoUpdate(float deltaTime) {
            for (var i = 0; i < _entities.Count; i++) {
                if (_entities[i].DoUpdate(deltaTime)) continue;
                var removedEntity = _entities[i];
                _entities[i] = _entities[^1];
                _entities.RemoveAt(_entities.Count - 1);
                OnEntityRemoved?.Invoke(removedEntity);
                i--;
            }
        }

        public void Reset() {
            Player = null;
            foreach (var entity in _entities) {
                entity.Destroy();
            }
            _entities.Clear();
        }

        public void AddPlayer() {
            var player = _entitiesFactory.GetEntity("Player", EntityType.Player);
            if (player == null) {
                _logger?.LogError("Fail to build player");
                return;
            }
            _entities.Add(player);
            Player = (ITankModel) player;
        }

        public void AddEnemies(int amount) {
            for (var i = 0; i < amount; i++) {
                var enemy = _entitiesFactory.GetEntity($"Enemy_{i + 1}", EntityType.Enemy);
                if (enemy == null) {
                    _logger?.LogError("Fail to build enemy");
                    continue;
                }
                _entities.Add(enemy);
            }
        }

        public int GetEntitiesCount(EntityType entityType) {
            var result = 0;
            foreach (var entity in _entities) {
                if (entity?.Type == entityType) {
                    result++;
                }
            }
            return result;
        }
    }
}