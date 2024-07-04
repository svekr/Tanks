using com.Tanks.TanksBattle.Tank.Movement.Input;
using com.Tanks.TanksBattle.Tank.Physics;

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
            _input = input;
            _velocity = velocity;
            SetPhysicsModel(model);
            IsActive = true;
        }

        public void SetPhysicsModel(ITankPhysics model) {
            if (model == null || model == _model) return;
            _model?.RemoveUpdatable(this);
            _model = model;
            _model.AddUpdatable(this);
        }

        public bool DoUpdate(float deltaTime) {
            if (IsActive) {
                _input?.DoUpdate(deltaTime);
                UpdateModel(deltaTime);
            }
            return !_isDestroyed;
        }

        public void Destroy() {
            if (_isDestroyed) return;
            if (_input != null) {
                _input.Destroy();
                _input = default;
            }
            if (_model != null) {
                _model.RemoveUpdatable(this);
                _model = default;
            }
            _isDestroyed = true;
        }

        abstract protected void UpdateModel(float deltaTime);
    }
}