using com.Tanks.TanksBattle.Controllers.Updater;
using UnityEngine;

namespace com.Tanks.TanksBattle.Controllers.Tank.View {
    public class TankView : UpdatedExternalComponent, ITankView {
        public Transform Transform => _transform;

        [SerializeField] private Transform _transform;

        public void Destroy() {
            GameObject.Destroy(gameObject);
        }
    }
}