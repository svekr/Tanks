using System;
using UnityEngine;

namespace com.Tanks.TanksBattle.Game.GameEntity {
    public interface IEntityEventProvider {
        event Action<IGameEntity, Vector3> OnContact;
        event Action OnDestroyAnimated;

        void InvokeContact(IGameEntity other, Vector3 contactPoint);
        void InvokeDestroyAnimated();
        void Destroy();
    }
}