using System;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Movement;

namespace com.Tanks.TanksBattle.Tank.Events {
    public interface ITankEventProvider : IEntityEventProvider {
        event Action<ITankModel> OnHit;
        event Action<TankMovementType> OnChangeMovementType;

        void InvokeHit(ITankModel shooter);
        void InvokeChangeMovementType(TankMovementType type);
    }
}