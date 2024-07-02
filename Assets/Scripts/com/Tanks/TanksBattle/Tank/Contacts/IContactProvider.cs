using System;
using com.Tanks.TanksBattle.Game.GameEntity;

namespace com.Tanks.TanksBattle.Tank.Contacts {
    public interface IContactProvider {
        event Action<IGameEntityView> ContactHandler;
    }
}