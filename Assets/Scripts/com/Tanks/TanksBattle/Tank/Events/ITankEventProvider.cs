using System;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Movement;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Events {
    public interface ITankEventProvider {
        event Action<IGameEntity, Vector3> OnContact;
        event Action<TankMovementType> OnChangeMovementType;

        void InvokeContact(IGameEntity other, Vector3 contactPoint);
        void ChangeMovementType(TankMovementType type);
        void Destroy();
    }
}