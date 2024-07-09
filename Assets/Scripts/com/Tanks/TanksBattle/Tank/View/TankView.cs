using com.Tanks.TanksBattle.Game.GameEntity;
using UnityEngine;
using Utils.Components;

namespace com.Tanks.TanksBattle.Tank.View {
    [RequireComponent(typeof(Rigidbody))]
    public class TankView : MonoBehaviour, ITankView {
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private SelfDestroyer _destroyEffect;

        private IEntityEventProvider _eventProvider;
        private bool _isDestroyed;

        public Transform Transform => _transform;
        public IEntityEventProvider EventProvider => _eventProvider;
        public Transform MuzzleTransform => _muzzle;

        public void SetEventProvider(IEntityEventProvider value) {
            if (_eventProvider != null) {
                _eventProvider.OnDestroyAnimated -= OnDestroyAnimated;
            }
            _eventProvider = value;
            if (_eventProvider != null) {
                _eventProvider.OnDestroyAnimated += OnDestroyAnimated;
            }
        }

        public void Destroy() {
            if (_isDestroyed) return;
            _isDestroyed = true;
            SetEventProvider(null);
            gameObject.SetActive(false);
            GameObject.Destroy(gameObject);
        }

        private void OnDestroyAnimated() {
            Instantiate(_destroyEffect, transform.position, transform.rotation, transform.parent);
        }
    }
}