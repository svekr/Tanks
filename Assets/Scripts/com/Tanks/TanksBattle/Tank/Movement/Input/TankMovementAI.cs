using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Game.Settings;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Physics;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Movement.Input {
    abstract public class TankMovementAI : ITankMovementInput {
        private const float SPREAD_ANGLE_INITIAL = 60f;
        private const float SPREAD_ANGLE_CONTACT = 45f;
        private const float MIN_TIME_BETWEEN_CONTACTS = 0.8f;

        private readonly TankAIMovementSettings _aiSettings;
        private readonly ITankPhysics _tankPhysics;
        private readonly ITankEventProvider _eventProvider;
        private readonly IReadOnlyList<IGameEntity> _entities;

        private bool _isActive;
        private IGameEntity _player;
        private Quaternion _direction;
        private Vector3 _contactDirection;
        private float _timeToChangeDirection;
        private float _timeToAllowContact;

        protected float DeltaAngle { get; private set; }
        protected float DeltaForward { get; private set; }

        protected TankMovementAI(TankAIMovementSettings aiSettings, ITankPhysics physicsModel,
            ITankEventProvider eventProvider, IReadOnlyList<IGameEntity> entities) {
            _aiSettings = aiSettings;
            _tankPhysics = physicsModel;
            _eventProvider = eventProvider;
            _entities = entities;

            _eventProvider.OnContact += OnContact;
            ChangeDirection(Vector3.zero, SPREAD_ANGLE_INITIAL);
        }

        public void Destroy() {
            StopListenInput();
            _eventProvider.OnContact -= OnContact;
        }

        public void StartListenInput() {
            _isActive = true;
        }

        public void StopListenInput() {
            _isActive = false;
            ResetAxes();
        }

        public bool DoUpdate(float deltaTime) {
            if (!_isActive) return true;

            if (_timeToChangeDirection > 0) {
                _timeToChangeDirection -= deltaTime;
            } else {
                RedirectToTarget(GetTarget(EntityType.Player, ref _player));
            }

            if (_timeToAllowContact > 0f) {
                _timeToAllowContact -= deltaTime;
                DeltaForward = 0f;
            } else {
                DeltaForward = 1f;
            }

            DeltaAngle = NormalizeAngle(_direction.eulerAngles.y - _tankPhysics.Rotation.eulerAngles.y);
            DeltaAngle /= 180f;

            SetAxes();
            return true;
        }

        abstract protected void SetAxes();

        abstract protected void ResetAxes();

        private void RedirectToTarget(IGameEntity target) {
            if (target != null) {
                ChangeDirection(target.Position - _tankPhysics.Position, _aiSettings.PlayerHuntAccuracy);
            } else {
                ChangeDirection(Vector3.zero - _tankPhysics.Position, SPREAD_ANGLE_INITIAL);
            }
        }

        private void ChangeDirection(Vector3 direction, float spread) {
            var randomAngle = Random.Range(-spread, spread);
            _direction = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, randomAngle, 0f);
            _timeToChangeDirection = Random.Range(_aiSettings.StraightMovementTimeMin, _aiSettings.StraightMovementTimeMax);
        }

        private IGameEntity GetTarget(EntityType targetType, ref IGameEntity result) {
            if (result != null) {
                if (result.IsDestroyed || result.Type != targetType) {
                    result = null;
                } else {
                    return result;
                }
            }
            foreach (var entity in _entities) {
                if (entity?.Type != targetType) continue;
                if (entity.IsDestroyed) continue;
                result = entity;
                return result;
            }
            return null;
        }

        private float NormalizeAngle(float angle) {
            if (angle < 0f) {
                angle += 360f;
            }
            if (angle > 180f) {
                angle -= 360f;
            }
            return angle;
        }

        private void OnContact(IGameEntity other, Vector3 contactPoint) {
            if (_timeToAllowContact > 0f) {
                ChangeDirection(_contactDirection, 0f);
            } else {
                _timeToAllowContact = MIN_TIME_BETWEEN_CONTACTS;
                _contactDirection = _tankPhysics.Position - contactPoint;
                ChangeDirection(_contactDirection, SPREAD_ANGLE_CONTACT);
            }
        }
    }
}