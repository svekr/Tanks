using System;

namespace com.Tanks.TanksBattle.Controllers.Updater {
    public interface IUpdater {
        event Action<float> Updated;
    }
}