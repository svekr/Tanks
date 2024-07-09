using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.View;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Settings {
    [CreateAssetMenu(menuName = "Configs/Tank Config", fileName = "TankConfig")]
    public class TankConfig : ScriptableObject {
        [Header("View")]
        [SerializeField] private TankView _prefab;
        [Header("Movement")]
        [SerializeField] private TankMovementType _movementType = TankMovementType.Classic;
        [SerializeField] private float _movementVelocityLinear = 5f;
        [SerializeField] private float _movementVelocityAngular = 3f;
        [Header("Shooting")]
        [SerializeField] private float _reloadDuration = 2.5f;

        public TankView Prefab => _prefab;
        public ITankSettings GetSettings() {
            var result = new TankSettings {
                Movement = {
                    MovementType = _movementType,
                    Velocity = {
                        Linear = _movementVelocityLinear,
                        Angular = _movementVelocityAngular
                    }
                },
                Shooting = {
                    ReloadDuration = _reloadDuration
                }
            };
            return result;
        }
    }
}