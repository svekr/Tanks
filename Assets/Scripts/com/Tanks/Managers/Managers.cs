namespace com.Tanks.Managers {
    public class Managers {
        public InputManager.InputManager InputManager { get; private set; }

        public GameDataManager.GameDataManager GameDataManager { get; private set; }

        public void SetManager<T>(T manager, bool force = false) {
            var managerType = typeof(T);
            var properties = GetType().GetProperties();
            foreach (var property in properties) {
                if (property.PropertyType != managerType) continue;
                if (!force && property.GetValue(this) != null) return;
                property.SetValue(this, manager);
                return;
            }
        }
    }
}