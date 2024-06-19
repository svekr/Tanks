using System;

namespace com.Tanks.TanksBattle.Controllers.Tank.Input {
    public interface ITankMovementInput {
        event Action<float, float> OnInput;

        float Axis1 { get; }
        float Axis2 { get; }

        void Destroy();
    }
}