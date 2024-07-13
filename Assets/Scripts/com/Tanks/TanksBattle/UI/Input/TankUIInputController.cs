using System.Collections.Generic;
using com.Tanks.TanksBattle.Tank;
using com.Tanks.TanksBattle.Tank.Movement;
using UnityEngine;

namespace com.Tanks.TanksBattle.UI.Input {
    public class TankUIInputController : MonoBehaviour {
        [SerializeField] private MovementTypeSwitcher _movementTypeSwitcher;
        [SerializeField] private UIClickHandler _clickHandler;
        [SerializeField] private List<TankUIInput> _inputs;

        private TankUIInput _input;
        private ITankModel _player;

        public void SetPlayer(ITankModel player) {
            ResetPlayer();
            if (player?.EventProvider == null) {
                gameObject.SetActive(false);
                return;
            }
            _player = player;
            _player.EventProvider.OnChangeMovementType += OnChangeMovementType;
            if (_movementTypeSwitcher) {
                _movementTypeSwitcher.OnSwitch += OnSwitchMovementType;
                OnSwitchMovementType(_movementTypeSwitcher.CurrentType);
            }
            gameObject.SetActive(true);
        }

        private void OnDestroy() {
            ResetPlayer();
        }

        private void ResetPlayer() {
            if (_movementTypeSwitcher) {
                _movementTypeSwitcher.OnSwitch -= OnSwitchMovementType;
            }
            if (_player?.EventProvider == null) return;
            _player.EventProvider.OnChangeMovementType -= OnChangeMovementType;
            _player = null;
        }

        private void OnSwitchMovementType(TankMovementType movementType) {
            _player?.EventProvider?.InvokeChangeMovementType(movementType);
        }

        private void OnChangeMovementType(TankMovementType movementType) {
            if (_movementTypeSwitcher) {
                _movementTypeSwitcher.CurrentType = movementType;
            }
            SetInputType(movementType);
        }

        private void SetInputType(TankMovementType movementType) {
            var mousePresent = UnityEngine.Input.mousePresent;
            if (_clickHandler) {
                _clickHandler.SetClickHandler(InvokeShot);
                _clickHandler.gameObject.SetActive(mousePresent);
            }
            if (_input) {
                _input.Deactivate();
            }
            if (mousePresent) return;
            _input = _inputs.Find(input => input != null && input.MovementType == movementType);
            if (_input) {
                _input.SetShotButtonHandler(InvokeShot);
                _input.Activate();
            }
        }

        private void InvokeShot() {
            Main.Managers.InputManager.GetKeyDown.Simulate(KeyCode.Space, true);
        }
    }
}