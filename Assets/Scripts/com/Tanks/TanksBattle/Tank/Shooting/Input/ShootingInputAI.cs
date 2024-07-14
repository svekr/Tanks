using System;
using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Game.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace com.Tanks.TanksBattle.Tank.Shooting.Input {
    public class ShootingInputAI : ITankShootingInput {
        public event Action DoShot;

        private TankAIShootingSettings _aiSettings;
        private float _reloadDuration;
        private Transform _muzzle;
        private IReadOnlyList<IGameEntity> _entities;
        private float _shotCooldown;
        private bool _isActive;
        private IGameEntity _target;

        public ShootingInputAI(TankAIShootingSettings aiSettings, float reloadDuration, ITankModel model, IReadOnlyList<IGameEntity> entities) {
            _aiSettings = aiSettings ?? throw new ArgumentNullException(nameof(aiSettings));
            _reloadDuration = reloadDuration;
            _muzzle = model.TankView.MuzzleTransform;
            _entities = entities ?? throw new ArgumentNullException(nameof(entities));
        }

        public void StartListenInput() {
            _isActive = true;
        }

        public void StopListenInput() {
            _isActive = false;
        }

        public bool DoUpdate(float deltaTime) {
            if (_shotCooldown > 0) {
                _shotCooldown -= deltaTime;
                return true;
            }
            if (!_isActive) return true;
            UpdateTarget();
            if (IsTowardsTarget()) {
                InvokeShot();
            }
            return true;
        }

        public void Destroy() {
            StopListenInput();
            DoShot = null;
        }

        private void UpdateTarget() {
            if (_target != null && !_target.IsDestroyed) return;
            _target = null;
            foreach (var entity in _entities) {
                if (entity?.Type != EntityType.Player) continue;
                _target = entity;
            }
            if (_target?.IsDestroyed == true) {
                _target = null;
            }
        }

        private bool IsTowardsTarget() {
            if (_target == null || _target.IsDestroyed) return false;
            var angleToTarget = Vector3.Angle(_target.Position - _muzzle.position, _muzzle.forward);
            return angleToTarget < _aiSettings.Accuracy;
        }

        private void InvokeShot() {
            if (_shotCooldown > 0) return;
            var spread = Random.Range(0f, _aiSettings.ReloadTimeSpread);
            _shotCooldown = _reloadDuration + spread;
            DoShot?.Invoke();
        }
    }
}