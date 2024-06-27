using com.Tanks.TanksBattle.Tank.Movement.Input;
using com.Tanks.TanksBattle.Tank.Physics;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Movement {
    abstract public class TankMovementBase<TInput> : ITankMovement where TInput : ITankMovementInput {
        protected ITankPhysics _model;
        protected TInput _input;
        protected ITankVelocitySettings _velocity;

        private bool _isActive, _isDestroyed;

        abstract public TankMovementType MovementType { get; }

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

        protected TankMovementBase(ITankPhysics model, TInput input, ITankVelocitySettings velocity) {
            _model = model;
            _input = input;
            _velocity = velocity;
            _model.AddUpdatable(this);
            IsActive = true;
        }

        public bool DoUpdate(float deltaTime) {
            if (IsActive) {
                UpdateModel(deltaTime);
            }
            return !_isDestroyed;
        }

        public void Move(Vector3 position, Quaternion rotation) {
            _model?.Move(position, rotation);
        }

        public void Destroy() {
            if (_isDestroyed) return;
            if (_input != null) {
                _input.StopListenInput();
                _input = default;
            }
            if (_model != null) {
                _model = default;
            }
            _isDestroyed = true;
        }

        abstract protected void UpdateModel(float deltaTime);
    }
}