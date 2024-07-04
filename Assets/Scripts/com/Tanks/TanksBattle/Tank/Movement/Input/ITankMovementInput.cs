using Utils.Updatable;

namespace com.Tanks.TanksBattle.Tank.Movement.Input {
    public interface ITankMovementInput : IUpdatable{
        void StartListenInput();
        void StopListenInput();
        void Destroy();
    }
}