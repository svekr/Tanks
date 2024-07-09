using System;
using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Shooting;

namespace com.Tanks.TanksBattle.Tank.Settings {
    [Serializable]
    public class TankSettings : ITankSettings {
        public ITankMovementSettings Movement { get; } = new TankMovementSettings();
        public ITankShootingSettings Shooting { get; } = new TankShootingSettings();
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

    [Serializable]
    public class TankShootingSettings : ITankShootingSettings {
        public float ReloadDuration { get; set; }
    }
}