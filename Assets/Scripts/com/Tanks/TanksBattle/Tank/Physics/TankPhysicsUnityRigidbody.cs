using System;
using UnityEngine;
using Utils.Updatable;

namespace com.Tanks.TanksBattle.Tank.Physics {
    public class TankPhysicsUnityRigidbody : ITankPhysics {
        private readonly Rigidbody _rigidbody;
        private readonly Updater _updater;

        private bool _isDestroyed;

        public bool IsActive => !_isDestroyed && _rigidbody.gameObject.activeInHierarchy;

        public TankPhysicsUnityRigidbody(Transform transform) {
            if (transform == null) {
                throw new ArgumentNullException();
            }
            _rigidbody = transform.GetComponent<Rigidbody>();
            if (_rigidbody == null) {
                throw new ArgumentException("Given Transform doesn't have Rigidbody component");
            }
            _updater = new Updater();
        }

        public Vector3 Position {
            get => _rigidbody.position;
            set => _rigidbody.MovePosition(value);
        }

        public Quaternion Rotation {
            get => _rigidbody.rotation;
            set => _rigidbody.MoveRotation(value);
        }

        public Vector3 Velocity {
            get => _rigidbody.velocity;
            set => _rigidbody.velocity = value;
        }

        public Vector3 AngularVelocity {
            get => _rigidbody.angularVelocity;
            set => _rigidbody.angularVelocity = value;
        }

        public void Move(Vector3 position, Quaternion rotation) {
            _rigidbody.Move(position, rotation);
        }

        public void Destroy() {
            _isDestroyed = true;
            Clear();
        }

        public void Clear() {
            _updater.Clear();
        }

        public bool DoUpdate(float deltaTime) {
            if (IsActive) {
                _updater.DoUpdate(deltaTime);
            }
            return !_isDestroyed;
        }

        public void AddUpdatable(IUpdatable updatable) {
            _updater.AddUpdatable(updatable);
        }

        public void RemoveUpdatable(IUpdatable updatable) {
            _updater.RemoveUpdatable(updatable);
        }
    }
}