using System;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Movement;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Events {
    public class TankEventProvider : ITankEventProvider {
        public event Action<IGameEntity, Vector3> OnContact;
        public event Action OnDestroyAnimated;
        public event Action<ITankModel> OnHit;
        public event Action<TankMovementType> OnChangeMovementType;

        public void InvokeContact(IGameEntity other, Vector3 contactPoint) {
            OnContact?.Invoke(other, contactPoint);
        }

        public void InvokeDestroyAnimated() {
            OnDestroyAnimated?.Invoke();
        }

        public void InvokeHit(ITankModel shooter) {
            OnHit?.Invoke(shooter);
        }

        public void InvokeChangeMovementType(TankMovementType type) {
            OnChangeMovementType?.Invoke(type);
        }

        public void Destroy() {
            OnContact = null;
            OnChangeMovementType = null;
        }
    }
}