using com.Tanks.TanksBattle.Tank.Settings;

namespace com.Tanks.TanksBattle.Tank.Builder {
    public class PlayerTankBuilder : TankBuilder {
        private ITankModelBuilder _modelBuilder;
        override protected ITankModelBuilder ModelBuilder => _modelBuilder ??= new PlayerTankModelBuilder();

        override protected string GetName(TankConfig config) {
            return $"Player_{config.TankView.name}";
        }
    }
}