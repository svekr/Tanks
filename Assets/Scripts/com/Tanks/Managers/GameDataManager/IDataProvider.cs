using System;

namespace com.Tanks.Managers.GameDataManager {
    public interface IDataProvider {
        void LoadData<T>(Action<T> onLoadComplete, string path);

        void SaveData<T>(T data, string path);
    }
}