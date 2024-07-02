using System;
using com.Tanks.TanksBattle.Tank.Movement;

namespace com.Tanks.TanksBattle.Tank.Settings {
    [Serializable]
    public class TankSettings : ITankSettings {
        public ITankMovementSettings Movement { get; } = new TankMovementSettings();
    }

    [Serializable]
    public class TankMovementSettings : ITankMovementSettings {
        public TankMovementType MovementType { get; set; } = TankMovementType.Immovable;
        public ITankVelocitySettings Velocity { get; } = new TankVelocitySettings();
    }

    [Serializable]
    public class TankVelocitySettings : ITankVelocitySettings {
        public float Linear { get; set; }
        public float Angular { get; set; }
    }
}