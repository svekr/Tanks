using System;

namespace com.Tanks.TanksBattle.Controllers.Tank.Input {
    public interface ITankFireInput {
        event Action OnFire;

        void Destroy();
    }
}