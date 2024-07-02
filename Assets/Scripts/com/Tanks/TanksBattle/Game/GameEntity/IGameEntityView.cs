using UnityEngine;

namespace com.Tanks.TanksBattle.Game.GameEntity {
    public interface IGameEntityView {
        Transform Transform { get; }

        void Destroy();
    }
}