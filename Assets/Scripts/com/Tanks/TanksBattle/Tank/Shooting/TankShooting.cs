using com.Tanks.TanksBattle.Game;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Shooting.Input;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Shooting {
    public class TankShooting : ITankShooting {
        private GameContext _gameContext;
        private ITankModel _model;
        private ITankShootingInput _input;
        private bool _isActive, _isDestroyed;

        public bool IsActive {
            get => _isActive && !_isDestroyed && _input != null;

            set {
                if (value == _isActive) return;
                if (value) {
                    _input?.StartListenInput();
                } else {
                    _input?.StopListenInput();
                }
                _isActive = value;
            }
        }

        public TankShooting(ITankShootingInput input, GameContext gameContext, ITankModel _tankModel) {
            _input = input;
            _gameContext = gameContext;
            _input.DoShot += DoShot;
            _model = _tankModel;
            IsActive = true;
        }

        public bool DoUpdate(float deltaTime) {
            _input?.DoUpdate(deltaTime);
            return !_isDestroyed;
        }

        public void Destroy() {
            IsActive = false;
            _input?.Destroy();
            _input = null;
            _isDestroyed = true;
        }

        private void DoShot() {
            var projectile = _gameContext.ProjectileFactory.GetProjectile(_model.TankView.MuzzleTransform);
            projectile.transform.SetParent(_model.View.Transform.parent);
            projectile.DoShot(OnHit);
        }

        private void OnHit(IGameEntityView otherView, Vector3 contactPoint) {
            if (otherView?.EventProvider is not ITankEventProvider otherEventProvider) return;
            otherEventProvider.InvokeHit(_model);
        }
    }
}