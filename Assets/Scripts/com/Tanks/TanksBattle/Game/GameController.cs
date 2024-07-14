using System;
using System.Collections;
using com.Tanks.TanksBattle.Game.Data;
using com.Tanks.TanksBattle.Game.Environment;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank;
using UnityEngine;

namespace com.Tanks.TanksBattle.Game {
    public class GameController : MonoBehaviour {
        public event Action<ITankModel> OnPlayerCreated;

        [SerializeField] private GameContext _gameContext;
        [SerializeField] private GameEnvironment _gameEnvironment;
        [SerializeField] private float _playerRespawnDelay = 1f;

        private ILogger _logger;
        private int _enemiesAmount;
        private GameModel _gameModel;

        public void StartGame(ILogger logger, int enemiesAmount, GameModelData savedData = null) {
            _logger = logger;
            _enemiesAmount = enemiesAmount;
            SetupGameModel();
            if (TrySetSavedData(savedData)) return;
            _gameModel.AddEnemies(enemiesAmount);
            AddPlayer();
        }

        public void DoUpdate(float deltaTime) {
            _gameModel?.DoUpdate(deltaTime);
        }

        public GameModelData GetGameModelData() {
            return _gameModel.GetData();
        }

        private bool TrySetSavedData(GameModelData savedData) {
            if (savedData == null) return false;
            if (savedData.GetEntitiesCount(EntityType.Enemy) == 0) {
                _gameModel.AddEnemies(_enemiesAmount);
            }
            if (savedData.GetEntitiesCount(EntityType.Player) == 0) {
                _gameModel.AddPlayer();
            }
            _gameModel.SetData(savedData);
            OnPlayerCreated?.Invoke(_gameModel.Player);
            return true;
        }

        private void SetupGameModel() {
            if (_gameModel == null) {
                _gameModel = new GameModel(_logger, _gameContext, _gameEnvironment);
                _gameModel.OnEntityRemoved += OnEntityRemoved;
            } else {
                _gameModel.Reset();
            }
        }

        private void AddPlayer() {
            _gameModel.AddPlayer();
            OnPlayerCreated?.Invoke(_gameModel.Player);
        }

        private void OnEntityRemoved(IGameEntity entity) {
            if (entity?.Type == EntityType.Enemy) {
                if (_gameModel.GetEntitiesCount(entity.Type) > 0) return;
                _gameModel.AddEnemies(_enemiesAmount);
                return;
            }
            if (entity?.Type == EntityType.Player) {
                StartCoroutine(AddPlayerDelayed());
            }
        }

        private IEnumerator AddPlayerDelayed() {
            yield return new WaitForSeconds(_playerRespawnDelay);
            AddPlayer();
        }
    }
}