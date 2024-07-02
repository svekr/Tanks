using System;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Movement;

namespace com.Tanks.TanksBattle.Tank.Events {
    public class TankEventProvider : ITankEventProvider {
        public event Action<IGameEntity> OnContact;
        public event Action<TankMovementType> OnChangeMovementType;

        public void InvokeContact(IGameEntity other) {
            OnContact?.Invoke(other);
        }

        public void ChangeMovementType(TankMovementType type) {
            OnChangeMovementType?.Invoke(type);
        }

        public void Destroy() {
            OnContact = null;
            OnChangeMovementType = null;
        }
    }
}