using System;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Contacts;
using UnityEngine;
using Utils.Components;
using Utils.Pool;

namespace com.Tanks.TanksBattle.Tank.Shooting.Projectile {

    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : PoolableMonoBehaviour {
        [SerializeField] private float _velocity = 60f;

        private Rigidbody _rigidbody;
        private IContactProvider _contactProvider;
        private Func<SelfDestroyer> _getExplosion;
        private Action<IGameEntityView, Vector3> _hitHandler;

        public void SetExplosion(Func<SelfDestroyer> explosionAccessor) {
            _getExplosion = explosionAccessor;
        }

        public void DoShot(Action<IGameEntityView, Vector3> hitHandler) {
            _hitHandler = hitHandler;
            _rigidbody.velocity = transform.forward * _velocity;
        }

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _contactProvider = GetContactProvider(transform);
            _contactProvider.ContactHandler += OnContact;
        }

        private IContactProvider GetContactProvider(Transform transform) {
            if (transform == null) {
                throw new ArgumentNullException();
            }
            var contactProvider = transform.GetComponent<ContactProviderUnity>();
            if (contactProvider == null) {
                contactProvider = transform.gameObject.AddComponent<ContactProviderUnity>();
            }
            return contactProvider;
        }

        private void OnContact(IGameEntityView otherView, Vector3 contactPoint) {
            _rigidbody.velocity = Vector3.zero;
            if (!gameObject.activeSelf) return;
            _hitHandler?.Invoke(otherView, contactPoint);
            _hitHandler = null;
            InstantiateExplosion(contactPoint);
            ReturnToPool();
        }

        private void InstantiateExplosion(Vector3 position) {
            var explosion = _getExplosion?.Invoke();
            if (explosion == null) return;
            var rotation = Quaternion.LookRotation(transform.position - position);
            explosion.transform.SetParent(transform.parent);
            explosion.transform.SetPositionAndRotation(position, rotation);
        }
    }
}