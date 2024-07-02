using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using UnityEngine;

namespace com.Tanks.TanksBattle.Game.Environment.Spawn {
    public interface IInitialPositionProvider {
        void SetEnvironment(IGameEnvironment environment, List<IGameEntity> existingEntities);
        void SetEntityToInitialPosition(IGameEntity entity);
        bool TryGetEntityInitialPosition(EntityType entityType, string entityName, out Vector3 position, out Quaternion rotation);
    }
}