using com.Tanks.TanksBattle.Tank.Shooting.Projectile;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Components;
using Utils.Pool;

namespace com.Tanks.TanksBattle.Game.GameEntity.Factory {
    [CreateAssetMenu(menuName = "Configs/Tank Weapon", fileName = "Projectile")]
    public class ProjectileFactory : ScriptableObject {
        [SerializeField] private int _poolCapacity = 10;
        [Header("Prefabs")]
        [SerializeField] private Projectile _projectile;
        [SerializeField] private SelfDestroyer _projectileExplosion;
        [SerializeField] private SelfDestroyer _shotMuzzleEffect;

        private Transform _poolContainer;
        private GameObjectPool<Projectile> _projectilePool;
        private GameObjectPool<SelfDestroyer> _explosionPool;
        private GameObjectPool<SelfDestroyer> _shotPool;

        private Transform PoolContainer {
            get {
                if (_poolContainer != null) return _poolContainer;
                var poolObject = GameObject.Find("/Pool") ?? new GameObject("Pool");
                _poolContainer = poolObject.transform;
                return _poolContainer;
            }
        }

        public Projectile GetProjectile(Transform container) {
            _projectilePool ??= new GameObjectPool<Projectile>(_projectile, _poolCapacity, PoolContainer);
            var projectile = _projectilePool.Get();
            SetContainer(projectile, container);

            if (_shotMuzzleEffect != null) {
                _shotPool ??= new GameObjectPool<SelfDestroyer>(_shotMuzzleEffect, _poolCapacity, PoolContainer);
                SetContainer(_shotPool.Get(), container);
            }

            if (_projectileExplosion != null) {
                _explosionPool ??= new GameObjectPool<SelfDestroyer>(_projectileExplosion, _poolCapacity, PoolContainer);
                projectile.SetExplosion(_explosionPool.Get);
            }

            return projectile;
        }

        private void SetContainer(Component value, Transform container) {
            if (value == null || container == null) return;
            value.transform.SetParent(container);
            value.transform.SetPositionAndRotation(container.position, container.rotation);
        }
    }
}