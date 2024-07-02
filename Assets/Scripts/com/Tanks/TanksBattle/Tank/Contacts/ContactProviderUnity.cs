using System;
using com.Tanks.TanksBattle.Game.GameEntity;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Contacts {
    public class ContactProviderUnity : MonoBehaviour, IContactProvider {
        [SerializeField] private LayerMask _layerMask;

        public event Action<IGameEntityView> ContactHandler;

        private void OnCollisionEnter(Collision other) {
            ProcessContact(other.gameObject);
        }

        private void OnTriggerEnter(Collider other) {
            ProcessContact(other.gameObject);
        }

        private void ProcessContact(GameObject other) {
            if (!IsLayerSuitable(other)) return;
            ContactHandler?.Invoke(other.GetComponent<IGameEntityView>());
        }

        private bool IsLayerSuitable(GameObject other) {
            return (_layerMask.value & (1 << other.layer)) > 0;
        }

        private void OnDestroy() {
            ContactHandler = null;
        }
    }
}