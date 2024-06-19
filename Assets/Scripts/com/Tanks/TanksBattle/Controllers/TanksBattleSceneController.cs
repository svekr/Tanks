using com.Tanks.Managers.InputManager;
using com.Tanks.Managers.Logger;
using com.Tanks.Managers.SceneManagement;
using com.Tanks.TanksBattle.Controllers.Updater;
using UnityEngine;

namespace com.Tanks.TanksBattle.Controllers {
    public class TanksBattleSceneController : SceneController {
        [SerializeField] private UpdaterComponent _updater;
        [SerializeField] private PlayerBuilder _playerBuilder;

        private InputManager _inputManager;

        override protected void InitLogger() {
            Logger = new UnityLogger();
        }

        override protected void StartHandler() {
            _inputManager = new InputManager();
            _updater.Updated += _inputManager.OnUpdate;

            _playerBuilder.BuildTank(_updater, _inputManager);
        }

        override protected void DestroyHandler() {
            _updater.Updated -= _inputManager.OnUpdate;
        }
    }
}