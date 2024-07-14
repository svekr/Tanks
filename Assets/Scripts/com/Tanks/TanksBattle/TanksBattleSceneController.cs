using com.Tanks.Managers.GameDataManager;
using com.Tanks.Managers.InputManager;
using com.Tanks.Managers.Logger;
using com.Tanks.Managers.SceneManagement;
using com.Tanks.TanksBattle.Game;
using com.Tanks.TanksBattle.Game.Data;
using UnityEngine;
using Utils.Components;

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
            if (Main.Managers.GameDataManager == null) {
                var appEventsProvider = GetComponent<ApplicationEventsProvider>();
                Main.Managers.SetManager(new GameDataManager(Logger, new LocalDataProvider(Logger), appEventsProvider));
            }
        }

        override protected void StartHandler() {
            Main.Managers.GameDataManager.LoadData(StartGame);
            Main.Managers.GameDataManager.OnSaveDataRequest += OnSaveDataRequest;
        }

        override protected void DestroyHandler() {
            Main.Managers.GameDataManager.OnSaveDataRequest -= OnSaveDataRequest;
        }

        private void StartGame(GameModelData savedData) {
            _game.StartGame(Logger, 4, savedData);
        }

        private void OnSaveDataRequest() {
            Main.Managers.GameDataManager.SaveGameData(_game.GetGameModelData());
        }

        private void Update() {
            Main.Managers.InputManager?.DoUpdate(Time.deltaTime);
        }

        private void FixedUpdate() {
            _game.DoUpdate(Time.fixedDeltaTime);
        }
    }
}