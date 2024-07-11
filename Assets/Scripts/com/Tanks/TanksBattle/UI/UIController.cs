using com.Tanks.TanksBattle.Game;
using com.Tanks.TanksBattle.Tank;
using com.Tanks.TanksBattle.UI.Input;
using UnityEngine;

namespace com.Tanks.TanksBattle.UI {
    public class UIController : MonoBehaviour {
        [SerializeField] private GameController _gameController;
        [SerializeField] private TankUIInputController _inputController;

        private ITankModel _player;

        private void Awake() {
            _gameController.OnPlayerCreated += SetPlayer;
        }

        private void OnDestroy() {
            _gameController.OnPlayerCreated -= SetPlayer;
            SetPlayer(null);
        }

        private void SetPlayer(ITankModel player) {
            if (player == _player) return;
            RemovePlayerListeners();
            _player = player;
            AddPlayerListeners();
            _inputController.SetPlayer(_player);
        }

        private void AddPlayerListeners() {
            if (_player?.EventProvider == null) return;
            _player.EventProvider.OnDestroyAnimated += OnPlayerDestroy;
        }

        private void RemovePlayerListeners() {
            if (_player?.EventProvider == null) return;
            _player.EventProvider.OnDestroyAnimated -= OnPlayerDestroy;
        }

        private void OnPlayerDestroy() {
            SetPlayer(null);
        }
    }
}