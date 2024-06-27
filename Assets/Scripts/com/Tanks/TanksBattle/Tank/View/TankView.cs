using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.View {
    [RequireComponent(typeof(Rigidbody))]
    public class TankView : MonoBehaviour, ITankView {
        public Transform Transform => _transform;

        [SerializeField] private Transform _transform;

        public void Destroy() {
            GameObject.Destroy(gameObject);
        }
    }
}