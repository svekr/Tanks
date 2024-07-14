using System;
using com.Tanks.TanksBattle.Game.Data;
using Utils.Components;

namespace com.Tanks.Managers.GameDataManager {
    public class GameDataManager {
        public event Action OnSaveDataRequest;

        private readonly string _path = "GameData";
        private readonly IDataProvider _dataProvider;
        private readonly SaveDataRequester _saveDataRequester;

        public GameDataManager(ILogger logger, IDataProvider dataProvider, IApplicationEventsProvider appEventsProvider) {
            _dataProvider = dataProvider;
            _saveDataRequester = new SaveDataRequester(logger, InvokeSaveDataRequest);
            SetAppEventsProvider(appEventsProvider);
        }

        public void SetAppEventsProvider(IApplicationEventsProvider appEventsProvider) {
            _saveDataRequester.SetAppEventsProvider(appEventsProvider);
        }

        public void LoadData(Action<GameModelData> onLoadComplete) {
            _dataProvider.LoadData(onLoadComplete, _path);
        }

        public void SaveGameData(GameModelData data) {
            _dataProvider.SaveData(data, _path);
        }

        private void InvokeSaveDataRequest() {
            OnSaveDataRequest?.Invoke();
        }
    }
}