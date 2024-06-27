using com.Tanks.TanksBattle.Tank.Movement;

namespace com.Tanks.TanksBattle.Tank.Settings {
    public interface ITankSettings {
        ITankMovementSettings Movement { get; }
    }
}