using System;
using UnityEngine;

namespace com.Tanks.TanksBattle.Game.Settings {
    [CreateAssetMenu(menuName = "Configs/Enemy AI Config", fileName = "EnemiesAIConfig")]
    public class TanksAISettings : ScriptableObject {
        [SerializeField] private TankAIShootingSettings _shooting;

        public TankAIShootingSettings Shooting => _shooting;
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