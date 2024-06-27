using com.Tanks.Managers.InputManager;
using com.Tanks.Managers.Logger;
using com.Tanks.Managers.SceneManagement;
using com.Tanks.TanksBattle.Game;
using UnityEngine;

namespace com.Tanks.TanksBattle {
    public class TanksBattleSceneController : SceneController {
        [SerializeField] private GameController _game;

        override protected void InitLogger() {
            Logger = Main.Logger ??= new UnityLogger();
        }

        override protected void AwakeHandler() {
            if (Main.Managers.InputManager == null) {
                Main.Managers.SetManager(new InputManager());
            }
        }

        override protected void StartHandler() {
            _game.StartGame(Logger, 4);
        }

        private void Update() {
            Main.Managers.InputManager?.DoUpdate(Time.deltaTime);
        }

        private void FixedUpdate() {
            _game.DoUpdate(Time.fixedDeltaTime);
        }
    }
}