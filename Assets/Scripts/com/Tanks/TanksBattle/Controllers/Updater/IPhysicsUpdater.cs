using System;

namespace com.Tanks.TanksBattle.Controllers.Updater {
    public interface IPhysicsUpdater {
        event Action<float> PhysicsUpdated;
    }
}