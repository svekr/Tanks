using Utils.Updatable;

namespace com.Tanks.TanksBattle.Tank.Shooting {
    public interface ITankShooting : IUpdatable {
        bool IsActive { get; set; }

        void Destroy();
    }
}