using com.Tanks.TanksBattle.Game.Environment;
using com.Tanks.TanksBattle.Tank.Movement;
using UnityEngine;

namespace com.Tanks.TanksBattle.Game {
    public class GameController : MonoBehaviour {
        [SerializeField] private GameContext _gameContext;
        [SerializeField] private GameEnvironment _gameEnvironment;

        private ILogger _logger;
        private int _enemiesAmount = 0;
        private GameModel _gameModel;

        public void StartGame(ILogger logger, int enemiesAmount) {
            _logger = logger;
            _enemiesAmount = enemiesAmount;
            SetupGameModel();
            _gameModel.AddEnemies(enemiesAmount);
            _gameModel.AddPlayer();
            _gameContext.CameraControl.SetTarget(_gameModel.Player?.View?.Transform);

            Main.Managers.InputManager.GetKeyDown.AddListener(KeyCode.Alpha1, () => {
                _gameModel.Player?.EventProvider.ChangeMovementType(TankMovementType.Classic);
            });
            Main.Managers.InputManager.GetKeyDown.AddListener(KeyCode.Alpha2, () => {
                _gameModel.Player?.EventProvider.ChangeMovementType(TankMovementType.Caterpillar);
            });
        }

        public void DoUpdate(float deltaTime) {
            _gameModel?.DoUpdate(deltaTime);
        }

        [ContextMenu("Restart Game")]
        public void RestartGame() {
            StartGame(_logger, _enemiesAmount);
        }

        private void SetupGameModel() {
            if (_gameModel == null) {
                _gameModel = new GameModel(_logger, _gameContext, _gameEnvironment);
            } else {
                _gameModel.Reset();
            }
        }
    }
}