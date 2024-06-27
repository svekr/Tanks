using UnityEngine;

namespace com.Tanks.TanksBattle.Game.Environment {
    public interface IGameEnvironment {
        Bounds[] GetPlayerSpawnZones { get; }
        Bounds[] GetEnemiesSpawnZones { get; }
    }
}