using com.Tanks.TanksBattle.Tank.Settings;
using UnityEngine;
using Utils.CameraMovement;

namespace com.Tanks.TanksBattle.Game {
    public class GameContext : MonoBehaviour {
        [SerializeField] private TargetFollower _cameraControl;
        [SerializeField] private TankConfig _playerConfig;
        [SerializeField] private TankConfig _enemyConfig;
        [SerializeField] private Transform _tanksContainer;

        public TargetFollower CameraControl => _cameraControl;
        public Transform GameContainer => _tanksContainer;
        public TankConfig PlayerConfig => _playerConfig;
        public TankConfig EnemyConfig => _enemyConfig;
    }
}