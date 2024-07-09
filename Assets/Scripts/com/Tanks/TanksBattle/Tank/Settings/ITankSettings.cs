using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Shooting;

namespace com.Tanks.TanksBattle.Tank.Settings {
    public interface ITankSettings {
        ITankMovementSettings Movement { get; }
        ITankShootingSettings Shooting { get; }
    }
}