using com.Tanks.TanksBattle.Game.Environment;
using com.Tanks.TanksBattle.Game.Environment.Spawn;
using com.Tanks.TanksBattle.Tank.Builder;
using com.Tanks.TanksBattle.Tank.Settings;
using UnityEngine;
using Utils.CameraMovement;

namespace com.Tanks.TanksBattle.Game {
    public class GameController : MonoBehaviour {
        [SerializeField] private TargetFollower _cameraControl;
        [SerializeField] private TankConfig _playerConfig;
        [SerializeField] private TankConfig _enemyConfig;
        [SerializeField] private Transform _tanksContainer;
        [SerializeField] private GameEnvironment _environment;

        private ILogger _logger;
        private int _enemiesAmount = 0;
        private bool _isGamePaused = true;
        private GameModel _gameModel;
        private TankBuilder _playerTankBuilder;
        private TankBuilder _enemyTankBuilder;
        private TankSpawner _tankSpawner;

        public void StartGame(ILogger logger, int enemiesAmount) {
            _logger = logger;
            _enemiesAmount = enemiesAmount;
            SetupGameModel();
            AddEnemies(enemiesAmount);
            AddPlayer();
            PauseGame(false);
        }

        public void DoUpdate(float deltaTime) {
            if (_isGamePaused || _gameModel == null) return;
            _gameModel.DoUpdate(deltaTime);
        }

        [ContextMenu("Restart Game")]
        public void RestartGame() {
            PauseGame(true);
            StartGame(_logger, _enemiesAmount);
        }

        [ContextMenu("Pause Game")]
        public void PauseGame(bool pause) {
            _isGamePaused = pause;
        }

        private void SetupGameModel() {
            if (_gameModel == null) {
                _gameModel = new GameModel(_logger);
            } else {
                _gameModel.Reset();
            }
        }

        private void AddEnemies(int amount) {
            for (var i = 0; i < amount; i++) {
                AddEnemy();
            }
        }

        private void AddPlayer() {
            var builder = _playerTankBuilder ??= new PlayerTankBuilder();
            var tank = builder.BuildTank(_playerConfig, _tanksContainer);
            var spawner = _tankSpawner ??= new TankSpawner(_logger);
            spawner.SpawnTank(tank, _environment.SpawnZonesPlayer, _gameModel.Tanks);
            _gameModel.AddTank(tank);

            _cameraControl.SetTarget(tank.View.Transform);
        }

        private void AddEnemy() {
            var builder = _enemyTankBuilder ??= new EnemyTankBuilder();
            var tank = builder.BuildTank(_enemyConfig, _tanksContainer);
            var spawner = _tankSpawner ??= new TankSpawner(_logger);
            spawner.SpawnTank(tank, _environment.SpawnZonesEnemy, _gameModel.Tanks);
            _gameModel.AddTank(tank);
        }
    }
}