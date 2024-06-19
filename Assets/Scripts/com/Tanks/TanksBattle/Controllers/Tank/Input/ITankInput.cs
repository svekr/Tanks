namespace com.Tanks.TanksBattle.Controllers.Tank.Input {
    public interface ITankInput {
        ITankMovementInput Movement { get; }
        ITankFireInput Fire { get; }

        void Destroy();
    }
}