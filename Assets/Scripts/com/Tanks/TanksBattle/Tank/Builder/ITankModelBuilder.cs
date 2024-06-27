using com.Tanks.TanksBattle.Tank.Settings;
using com.Tanks.TanksBattle.Tank.View;

namespace com.Tanks.TanksBattle.Tank.Builder {
    public interface ITankModelBuilder {
        ITankModel BuildTank(string name, ITankView view, ITankSettings settings);
    }
}