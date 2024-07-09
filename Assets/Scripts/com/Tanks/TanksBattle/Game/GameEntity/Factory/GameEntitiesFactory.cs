using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.Environment;
using com.Tanks.TanksBattle.Game.Environment.Spawn;
using com.Tanks.TanksBattle.Tank;
using com.Tanks.TanksBattle.Tank.Builder;
using com.Tanks.TanksBattle.Tank.Settings;

namespace com.Tanks.TanksBattle.Game.GameEntity.Factory {
    public class GameEntitiesFactory {
        private readonly ILogger _logger;
        private readonly IInitialPositionProvider _positionProvider;

        private GameContext _context;
        private IGameEnvironment _environment;
        private List<IGameEntity> _entities;
        private ITankModelBuilder _playerBuilder;
        private ITankModelBuilder _enemyBuilder;

        public GameEntitiesFactory(ILogger logger) {
            _logger = logger;
            _positionProvider = new InitialPositionProvider(logger);
        }

        public void Initialize(GameContext context, IGameEnvironment environment, List<IGameEntity> entities) {
            _context = context;
            _environment = environment;
            _entities = entities;
            _positionProvider.SetEnvironment(_environment, _entities);
        }

        public IGameEntity GetEntity(string name, EntityType type) {
            var entity = BuildEntity(name, type);
            _positionProvider.SetEntityToInitialPosition(entity);
            return entity;
        }

        private IGameEntity BuildEntity(string name, EntityType type) {
            switch (type) {
                case EntityType.Player:
                    _playerBuilder ??= new PlayerTankModelBuilder(_entities, _context);
                    return BuildTank(name, _context.PlayerConfig, _playerBuilder);
                case EntityType.Enemy:
                    _enemyBuilder ??= new EnemyTankModelBuilder(_entities, _context);
                    return BuildTank(name, _context.EnemyConfig, _enemyBuilder);
                default:
                    _logger.LogError($"{GetType().Name}.{nameof(BuildEntity)}({name}, {type}): Invalid type");
                    return null;
            }
        }

        private ITankModel BuildTank(string name, TankConfig config, ITankModelBuilder builder) {
            var view = UnityEngine.Object.Instantiate(config.Prefab, _context.GameContainer);
            view.name = name;
            return builder.BuildTank(name, view, config.GetSettings());
        }
    }
}