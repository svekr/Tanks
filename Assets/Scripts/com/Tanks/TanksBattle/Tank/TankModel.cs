using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Builder;
using com.Tanks.TanksBattle.Tank.Contacts;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Physics;
using com.Tanks.TanksBattle.Tank.Settings;
using com.Tanks.TanksBattle.Tank.Shooting;
using com.Tanks.TanksBattle.Tank.View;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank {
    public class TankModel : ITankModel {
        public bool IsDestroyed { get; private set; }
        public EntityType Type { get; private set; }
        public string Name { get; private set; }
        public IGameEntityView View => TankView;
        public ITankView TankView { get; private set; }
        public ITankEventProvider EventProvider { get; private set; }
        public Vector3 Position => _physicsModel?.Position ?? Vector3.zero;

        private ITankSettings _settings;
        private ITankModelBuilder _builder;
        private ITankPhysics _physicsModel;
        private ITankMovement _movement;
        private ITankContactor _contactor;
        private ITankShooting _shooting;

        public TankModel(EntityType type, string name, ITankView view, ITankSettings settings, ITankModelBuilder builder) {
            Type = type;
            Name = name;
            TankView = view;
            _settings = settings;
            _builder = builder;

            EventProvider = new TankEventProvider();
            EventProvider.OnContact += OnContact;
            EventProvider.OnHit += OnHit;
            EventProvider.OnChangeMovementType += OnChangeMovementType;
            TankView.SetEventProvider(EventProvider);

            SetPhysics(_builder.BuildPhysics(this));
            SetMovement(_builder.BuildMovement(_physicsModel, _settings.Movement, EventProvider));
            _contactor = _builder.BuildContactor(this);
            _shooting = _builder.BuildShooting(this, _settings.Shooting);
        }

        public bool DoUpdate(float deltaTime) {
            if (IsDestroyed) return false;
            _shooting?.DoUpdate(deltaTime);
            return _physicsModel?.DoUpdate(deltaTime) == true;
        }

        public void SetPosition(Vector3 position, Quaternion rotation) {
            _physicsModel.Move(position, rotation);
        }

        public void Destroy() {
            if (IsDestroyed) return;
            IsDestroyed = true;
            TankView.Destroy();
            TankView = null;
            EventProvider.Destroy();
            _movement?.Destroy();
            _physicsModel?.Destroy();
            _physicsModel = null;
            _contactor?.Destroy();
            _shooting?.Destroy();
        }

        private void SetPhysics(ITankPhysics physicsModel) {
            if (physicsModel == null) return;
            _physicsModel?.RemoveUpdatable(_movement);
            _physicsModel = physicsModel;
            _movement?.SetPhysicsModel(_physicsModel);
        }

        private void SetMovement(ITankMovement movement) {
            if (movement == null) return;
            if (_movement != null && movement.MovementType == _movement.MovementType) return;
            _movement?.Destroy();
            _movement = movement;
            _movement.SetPhysicsModel(_physicsModel);
            EventProvider.InvokeChangeMovementType(_movement.MovementType);
        }

        private void OnContact(IGameEntity other, Vector3 contactPoint) {
            if (other == null) return;
            if (Type == EntityType.Player && other.Type == EntityType.Enemy) {
                EventProvider.InvokeDestroyAnimated();
                Destroy();
            }
        }

        private void OnHit(IGameEntity other) {
            if (other == null) return;
            if (Type == other.Type) return;
            EventProvider.InvokeDestroyAnimated();
            Destroy();
        }

        private void OnChangeMovementType(TankMovementType type) {
            if (_movement?.MovementType == type) return;
            _settings.Movement.MovementType = type;
            SetMovement(_builder.BuildMovement(_physicsModel, _settings.Movement, EventProvider));
        }
    }
}