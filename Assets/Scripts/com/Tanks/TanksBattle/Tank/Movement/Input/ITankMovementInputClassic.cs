namespace com.Tanks.TanksBattle.Tank.Movement.Input {
    public interface ITankMovementInputClassic : ITankMovementInput {
        float AxisVertical { get; }
        float AxisHorizontal { get; }
    }
}