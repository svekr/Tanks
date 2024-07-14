using com.Tanks.TanksBattle.Tank.Movement;
using UnityEngine;

namespace com.Tanks.TanksBattle.UI.Input {
    public class InputHint : MonoBehaviour {
        [SerializeField] private TankMovementType _movementType;
        [SerializeField] private MovementTypeSwitcher _switcher;

        private void Awake() {
            if (_switcher == null) return;
            gameObject.SetActive(_movementType == _switcher.CurrentType);
            _switcher.OnSwitch += OnSwitch;
        }

        private void OnDestroy() {
            if (_switcher == null) return;
            _switcher.OnSwitch -= OnSwitch;
        }

        private void OnSwitch(TankMovementType movementType) {
            gameObject.SetActive(_movementType == movementType);
        }
    }
}