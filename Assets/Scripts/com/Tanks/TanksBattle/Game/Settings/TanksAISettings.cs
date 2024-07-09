using System;
using UnityEngine;

namespace com.Tanks.TanksBattle.Game.Settings {
    [CreateAssetMenu(menuName = "Configs/Enemy AI Config", fileName = "EnemiesAIConfig")]
    public class TanksAISettings : ScriptableObject {
        [SerializeField] private TankAIMovementSettings _movement;
        [SerializeField] private TankAIShootingSettings _shooting;

        public TankAIMovementSettings Movement => _movement;
        public TankAIShootingSettings Shooting => _shooting;
    }

    [Serializable]
    public class TankAIMovementSettings {
        [SerializeField] private float _straightMovementTimeMin = 1.5f;
        [SerializeField] private float _straightMovementTimeMax = 3.5f;
        [Tooltip("Greater value means less accuracy")]
        [Range(0f, 180f)]
        [SerializeField] private float _playerHuntAccuracy = 10f;

        public float PlayerHuntAccuracy => _playerHuntAccuracy;
        public float StraightMovementTimeMin => _straightMovementTimeMin;
        public float StraightMovementTimeMax => _straightMovementTimeMax;
    }

    [Serializable]
    public class TankAIShootingSettings {
        [Tooltip("Greater value means less accuracy")]
        [SerializeField] private float _accuracy = 10f;
        [Range(0f, 60f)]
        [SerializeField] private float _reloadTimeSpread = 1f;

        public float Accuracy => _accuracy;
        public float ReloadTimeSpread => _reloadTimeSpread;
    }
}