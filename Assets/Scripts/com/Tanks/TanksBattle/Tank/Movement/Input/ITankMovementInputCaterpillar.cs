namespace com.Tanks.TanksBattle.Tank.Movement.Input {
    public interface ITankMovementInputCaterpillar : ITankMovementInput {
        float AxisLeft { get; }
        float AxisRight { get; }
    }
}