namespace com.Tanks.TanksBattle.Tank.Movement {
    public interface ITankMovementSettings {
        TankMovementType MovementType { get; set; }
        ITankVelocitySettings Velocity { get; }
    }
}