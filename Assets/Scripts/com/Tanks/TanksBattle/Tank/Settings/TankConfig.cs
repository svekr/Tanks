using System;
using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.View;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Settings {
    [CreateAssetMenu(menuName = "Configs/Tank Configs", fileName = "TankConfig")]
    public class TankConfig : ScriptableObject, ITankSettings {
        [SerializeField] private TankView _tankView;
        [SerializeField] private TankMovementConfig _movement;

        public TankView TankView => _tankView;
        public ITankMovementSettings Movement => _movement;
    }

    [Serializable]
    public class TankMovementConfig : ITankMovementSettings {
        [SerializeField] private TankMovementType _movementType = TankMovementType.Classic;
        [SerializeField] private TankVelocityConfig _velocity;

        public TankMovementType MovementType => _movementType;
        public ITankVelocitySettings Velocity => _velocity;
    }

    [Serializable]
    public class TankVelocityConfig : ITankVelocitySettings {
        [SerializeField] private float _linear = 5f;
        [SerializeField] private float _angular = 3f;

        public float Linear => _linear;
        public float Angular => _angular;
    }
}