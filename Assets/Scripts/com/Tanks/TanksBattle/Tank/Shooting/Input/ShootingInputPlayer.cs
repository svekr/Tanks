using System;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Shooting.Input {
    public class ShootingInputPlayer : ITankShootingInput {
        public event Action DoShot;

        private float _reloadDuration;
        private float _shotCooldown;

        public ShootingInputPlayer(float reloadDuration) {
            _reloadDuration = reloadDuration;
        }

        public void StartListenInput() {
            Main.Managers.InputManager.GetKeyDown.AddListener(KeyCode.Mouse0, InvokeShot);
            Main.Managers.InputManager.GetKeyDown.AddListener(KeyCode.Space, InvokeShot);
        }

        public void StopListenInput() {
            Main.Managers.InputManager.GetKeyDown.RemoveListener(KeyCode.Mouse0, InvokeShot);
            Main.Managers.InputManager.GetKeyDown.RemoveListener(KeyCode.Space, InvokeShot);
        }

        public bool DoUpdate(float deltaTime) {
            if (_shotCooldown > 0) {
                _shotCooldown -= deltaTime;
            }
            return true;
        }

        public void Destroy() {
            StopListenInput();
            DoShot = null;
        }

        private void InvokeShot() {
            if (_shotCooldown > 0) return;
            _shotCooldown = _reloadDuration;
            DoShot?.Invoke();
        }
    }
}