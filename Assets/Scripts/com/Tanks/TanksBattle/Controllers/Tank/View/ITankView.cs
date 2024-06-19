using com.Tanks.TanksBattle.Controllers.Updater;
using UnityEngine;

namespace com.Tanks.TanksBattle.Controllers.Tank.View {
    public interface ITankView : ICommonUpdater, IUpdatable, IPhysicsUpdatable {
        Transform Transform { get; }

        void SetExternalUpdater(ICommonUpdater updater);
        void Destroy();
    }
}