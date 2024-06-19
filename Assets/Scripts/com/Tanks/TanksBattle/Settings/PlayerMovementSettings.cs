using System;
using com.Tanks.TanksBattle.Controllers.Tank.Movement;
using UnityEngine;

namespace com.Tanks.TanksBattle.Settings {
    [CreateAssetMenu(menuName = "Settings/Tank Settings/Player Movement Settings", fileName = "PlayerMovementSettings")]
    public class PlayerMovementSettings : ScriptableObject {
        [SerializeField] private MovementType _movementType;
        [SerializeField] private TankSpeedSettings _speed;
        [SerializeField] private TankInputSettings _input;

        public MovementType MovementType => _movementType;
        public TankSpeedSettings Speed => _speed;
        public TankInputSettings Input => _input;
    }

    [Serializable]
    public class TankSpeedSettings {
        [SerializeField] private float _linear = 5f;
        [SerializeField] private float _angular = 20f;

        public float Linear => _linear;
        public float Angular => _angular;
    }

    [Serializable]
    public class TankInputSettings {
        [SerializeField] private string _axis1Name = "Vertical";
        [SerializeField] private string _axis2Name = "Horizontal";
        [SerializeField] private KeyCode _fireKey = KeyCode.Space;

        public string Axis1Name => _axis1Name;
        public string Axis2Name => _axis2Name;
        public KeyCode FireKey => _fireKey;
    }
}