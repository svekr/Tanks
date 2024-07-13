using System;
using com.Tanks.TanksBattle.Tank.Movement;
using UnityEngine;

namespace com.Tanks.TanksBattle.UI.Input {
    abstract public class TankUIInput : MonoBehaviour {
        private bool _isActive;
        private Action _shotButtonHandler;

        abstract public TankMovementType MovementType { get; }

        public void Activate() {
            if (_isActive) return;
            _isActive = true;
            gameObject.SetActive(true);
            Reset();
            AddListeners();
        }

        public void Deactivate() {
            if (!_isActive) return;
            _isActive = false;
            gameObject.SetActive(false);
            RemoveListeners();
        }

        public void SetShotButtonHandler(Action shotButtonHandler) {
            _shotButtonHandler = shotButtonHandler;
        }

        abstract protected void Reset();

        abstract protected void AddListeners();

        abstract protected void RemoveListeners();

        abstract protected void SimulateInput();

        protected void OnShotButtonClick() {
            if (!_isActive) return;
            _shotButtonHandler?.Invoke();
        }

        private void Update() {
            SimulateInput();
        }
    }
}