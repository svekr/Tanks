using UnityEngine;
using Utils.Updatable;

namespace com.Tanks.TanksBattle.Game.GameEntity {
    public interface IGameEntity : IUpdatable {
        EntityType Type { get; }
        string Name { get; }
        IGameEntityView View { get; }
        Vector3 Position { get; }

        void SetPosition(Vector3 position, Quaternion rotation);
        void Destroy();
    }
}