using com.Tanks.TanksBattle.Game;
using com.Tanks.TanksBattle.Tank;
using UnityEngine;

namespace Utils.Components {
    public class PlayerFollower : MonoBehaviour {
        [SerializeField] private TargetFollower _follower;
        [SerializeField] private GameController _gameController;

        private void Start() {
            if (_gameController == null) return;
            _gameController.OnPlayerCreated += OnPlayerCreated;
        }

        private void OnDestroy() {
            if (_gameController == null) return;
            _gameController.OnPlayerCreated -= OnPlayerCreated;
        }

        private void OnPlayerCreated(ITankModel player) {
            if (_follower == null) return;
            _follower.SetTarget(player?.View?.Transform);
        }
    }
}