namespace com.Tanks.TanksBattle.Tank.Movement {
    public interface ITankMovementSettings {
        TankMovementType MovementType { get; }
        ITankVelocitySettings Velocity { get; }
    }
}