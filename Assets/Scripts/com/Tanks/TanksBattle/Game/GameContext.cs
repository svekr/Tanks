﻿using com.Tanks.TanksBattle.Game.GameEntity.Factory;
using com.Tanks.TanksBattle.Game.Settings;
using com.Tanks.TanksBattle.Tank.Settings;
using UnityEngine;

namespace com.Tanks.TanksBattle.Game {
    public class GameContext : MonoBehaviour {
        [SerializeField] private TankConfig _playerConfig;
        [SerializeField] private TankConfig _enemyConfig;
        [SerializeField] private Transform _tanksContainer;
        [SerializeField] private ProjectileFactory _projectileFactory;
        [SerializeField] private TanksAISettings _tanksAISettings;

        public Transform GameContainer => _tanksContainer;
        public TankConfig PlayerConfig => _playerConfig;
        public TankConfig EnemyConfig => _enemyConfig;
        public ProjectileFactory ProjectileFactory => _projectileFactory;
        public TanksAISettings AISettings => _tanksAISettings;
    }
}