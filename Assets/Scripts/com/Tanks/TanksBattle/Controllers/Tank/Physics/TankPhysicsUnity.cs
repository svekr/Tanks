using UnityEngine;

namespace com.Tanks.TanksBattle.Controllers.Tank.Physics {
    public class TankPhysicsUnity : ITankPhysics {
        private readonly Rigidbody _rigidbody;

        private bool _isDestroyed;

        public bool IsActive => !_isDestroyed && _rigidbody.gameObject.activeInHierarchy;

        public Transform Transform { get; }

        public TankPhysicsUnity(Transform transform, Rigidbody rigidbody = null) {
            Transform = transform;
            _rigidbody = (rigidbody != null) ? rigidbody : Transform.GetComponent<Rigidbody>();
        }

        public Vector3 Position {
            get => _rigidbody.position;
            set => _rigidbody.MovePosition(value);
        }

        public Quaternion Rotation {
            get => _rigidbody.rotation;
            set => _rigidbody.MoveRotation(value);
        }

        public void Destroy() {
            _isDestroyed = true;
        }
    }
}