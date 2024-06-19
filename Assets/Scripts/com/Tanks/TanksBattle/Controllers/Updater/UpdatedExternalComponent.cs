using System;
using UnityEngine;

namespace com.Tanks.TanksBattle.Controllers.Updater {
    public class UpdatedExternalComponent : MonoBehaviour, ICommonUpdater, IUpdatable, IPhysicsUpdatable {
        public event Action<float> Updated;
        public event Action<float> PhysicsUpdated;

        private ICommonUpdater _updater;

        public void OnUpdate(float deltaTime) {
            Updated?.Invoke(deltaTime);
        }

        public void OnPhysicsUpdate(float deltaTime) {
            PhysicsUpdated?.Invoke(deltaTime);
        }

        public void SetExternalUpdater(ICommonUpdater updater) {
            RemoveUpdater();
            if (updater == null) return;
            _updater = updater;
            updater.Updated += OnUpdate;
            _updater.PhysicsUpdated += OnPhysicsUpdate;
        }

        private void OnDestroy() {
            RemoveListeners();
            RemoveUpdater();
        }

        private void RemoveListeners() {
            Updated = null;
            PhysicsUpdated = null;
        }

        private void RemoveUpdater() {
            if (_updater == null) return;
            _updater.Updated -= OnUpdate;
            _updater.PhysicsUpdated -= OnPhysicsUpdate;
            _updater = null;
        }
    }
}