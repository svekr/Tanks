using com.Tanks.TanksBattle.Tank.Settings;

namespace com.Tanks.TanksBattle.Tank.Builder {
    public class EnemyTankBuilder : TankBuilder {
        private ITankModelBuilder _modelBuilder;

        override protected ITankModelBuilder ModelBuilder => _modelBuilder ??= new EnemyTankModelBuilder();

        override protected string GetName(TankConfig config) {
            return $"Enemy_{config.TankView.name}";
        }
    }
}