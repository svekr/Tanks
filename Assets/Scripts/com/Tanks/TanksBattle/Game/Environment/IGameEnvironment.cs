using com.Tanks.TanksBattle.Game.Environment.Spawn;
using com.Tanks.TanksBattle.Game.GameEntity;

namespace com.Tanks.TanksBattle.Game.Environment {
    public interface IGameEnvironment {
        SpawnZone[] GetSpawnZones(EntityType entityType);
    }
}