namespace Utils.Updatable {
    public interface IUpdater : IUpdatable {
        void AddUpdatable(IUpdatable updatable);
        void RemoveUpdatable(IUpdatable updatable);
        void Clear();
    }
}