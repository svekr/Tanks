using UnityEngine;

namespace Utils.CameraMovement {
    [RequireComponent(typeof(Transform))]
    public class TargetFollower : MonoBehaviour {
        [SerializeField] private Transform _target;
        [SerializeField] private LayerMask _layerMask = -1;
        [SerializeField] private float _colliderRadius = 0.5f;
        [SerializeField, Range(0f, 1f)] private float _anglesCollisionImpact = 0.7f;

        private Transform _transform;
        private Vector3 _position, _positionOffset;
        private Quaternion _rotation, _rotationOffset;

        public void SetTarget(Transform target) {
            _target = target;
        }

        public void MoveToInitialPosition() {
            _transform.SetPositionAndRotation(_positionOffset, _rotationOffset);
        }

        private void Start() {
            _transform = GetComponent<Transform>();
            _positionOffset = _transform.position;
            _rotationOffset = _transform.rotation;
        }

        private void Update() {
            if (!_target) return;
            var targetPosition = _target.position;
            var targetRotation = _target.rotation;
            _position = targetPosition + (targetRotation * _positionOffset);
            _rotation = targetRotation * _rotationOffset;
            var pointOverTarget = new Vector3(targetPosition.x, _position.y, targetPosition.z);
            var castDirection = _position - pointOverTarget;
            var castDistance = castDirection.magnitude;
            if (Physics.SphereCast(
                    pointOverTarget,
                    _colliderRadius,
                    castDirection,
                    out var hit,
                    castDistance,
                    _layerMask.value,
                    QueryTriggerInteraction.Ignore
                    )) {
                _position = hit.point + hit.normal * _colliderRadius;
                _position.y = pointOverTarget.y;
                var newCastDistance = (_position - pointOverTarget).magnitude;
                var rate = newCastDistance / castDistance;
                rate = _anglesCollisionImpact * (1 - rate * rate);
                _position.y += _colliderRadius * Mathf.Sqrt(castDistance * castDistance - newCastDistance * newCastDistance) * rate;
                _rotation = Quaternion.Lerp(_rotation, Quaternion.LookRotation(targetPosition - _position), rate);
            }
            _transform.SetPositionAndRotation(_position, _rotation);
        }
    }
}