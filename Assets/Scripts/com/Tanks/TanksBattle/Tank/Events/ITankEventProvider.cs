using System;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Movement;

namespace com.Tanks.TanksBattle.Tank.Events {
    public interface ITankEventProvider {
        event Action<IGameEntity> OnContact;
        event Action<TankMovementType> OnChangeMovementType;

        void InvokeContact(IGameEntity other);
        void ChangeMovementType(TankMovementType type);
        void Destroy();
    }
}