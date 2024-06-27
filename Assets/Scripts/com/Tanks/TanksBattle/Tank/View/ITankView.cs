using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.View {
    public interface ITankView {
        Transform Transform { get; }

        void Destroy();
    }
}