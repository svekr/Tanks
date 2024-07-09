using com.Tanks.TanksBattle.Game.GameEntity;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.View {
    public interface ITankView : IGameEntityView {
        Transform MuzzleTransform { get; }

        void SetEventProvider(IEntityEventProvider value);
    }
}