using System;
using UnityEngine;

namespace com.Tanks.TanksBattle.Controllers.Updater {
    public class UpdaterComponent : MonoBehaviour, ICommonUpdater {
        public event Action<float> Updated;
        public event Action<float> PhysicsUpdated;

        private void Update() {
            Updated?.Invoke(Time.deltaTime);
        }

        private void FixedUpdate() {
            PhysicsUpdated?.Invoke(Time.fixedDeltaTime);
        }

        private void OnDestroy() {
            Updated = null;
            PhysicsUpdated = null;
        }
    }
}