using System;
using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Events;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Contacts {
    public class TankContactorUnity : ITankContactor {
        private ITankEventProvider _tankEventProvider;
        private IReadOnlyList<IGameEntity> _entities;
        private IContactProvider _contactProvider;

        public TankContactorUnity(Transform transform, ITankEventProvider tankEventProvider, IReadOnlyList<IGameEntity> entities) {
            _contactProvider = GetContactProvider(transform);
            _contactProvider.ContactHandler += OnContact;
            _tankEventProvider = tankEventProvider;
            _entities = entities;
        }

        public void Destroy() {
            _tankEventProvider = null;
            _entities = null;
            if (_contactProvider == null) return;
            _contactProvider.ContactHandler -= OnContact;
            _contactProvider = null;
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
            if (otherView != null && _entities != null) {
                foreach (var entity in _entities) {
                    if (entity?.View != otherView) continue;
                    _tankEventProvider.InvokeContact(entity, contactPoint);
                    return;
                }
            }
            _tankEventProvider.InvokeContact(null, contactPoint);
        }
    }
}