using System;
using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;

namespace com.Tanks.TanksBattle.Game.Data {
    [Serializable]
    public class GameModelData {
        public List<EntityData> Entities;

        public int GetEntitiesCount(EntityType type) {
            if (Entities == null || Entities.Count == 0) return 0;
            var result = 0;
            foreach (var entity in Entities) {
                if (entity?.Type != type) continue;
                result++;
            }
            return result;
        }
    }
}