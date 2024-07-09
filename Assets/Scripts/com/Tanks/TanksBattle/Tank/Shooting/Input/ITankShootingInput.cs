using System;
using Utils.Updatable;

namespace com.Tanks.TanksBattle.Tank.Shooting.Input {
    public interface ITankShootingInput : IUpdatable {
        event Action DoShot;
        void StartListenInput();
        void StopListenInput();
        void Destroy();
    }
}