using System;
using com.Tanks.TanksBattle.Game.GameEntity;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Contacts {
    public interface IContactProvider {
        event Action<IGameEntityView, Vector3> ContactHandler;
    }
}