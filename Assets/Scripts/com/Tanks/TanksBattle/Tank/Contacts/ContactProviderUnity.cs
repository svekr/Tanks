using System;
using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Contacts {
    public class ContactProviderUnity : MonoBehaviour, IContactProvider {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private bool _ignoreTriggers;

        private readonly List<ContactPoint> _contacts = new ();

        public event Action<IGameEntityView, Vector3> ContactHandler;

        private void OnCollisionEnter(Collision other) {
            if (!IsLayerSuitable(other.gameObject)) return;
            ContactHandler?.Invoke(other.gameObject.GetComponent<IGameEntityView>(), GetContactPoint(other));
        }

        private void OnTriggerEnter(Collider other) {
            if (_ignoreTriggers || !IsLayerSuitable(other.gameObject)) return;
            ContactHandler?.Invoke(other.gameObject.GetComponent<IGameEntityView>(), GetContactPoint(other));
        }

        private bool IsLayerSuitable(GameObject other) {
            return (_layerMask.value & (1 << other.layer)) > 0;
        }

        private Vector3 GetContactPoint(Collision other) {
            other.GetContacts(_contacts);
            var result = Vector3.zero;
            var maxImpulse = 0f;
            foreach (var contactPoint in _contacts) {
                var impulse = contactPoint.impulse.magnitude;
                if (impulse <= maxImpulse) continue;
                maxImpulse = impulse;
                result = contactPoint.point;
            }
            return result;
        }

        private Vector3 GetContactPoint(Collider other) {
            return other.ClosestPointOnBounds(transform.position);
        }

        private void OnDestroy() {
            ContactHandler = null;
        }
    }
}