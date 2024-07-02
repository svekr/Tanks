using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.View {
    [RequireComponent(typeof(Rigidbody))]
    public class TankView : MonoBehaviour, ITankView {
        [SerializeField] private Transform _transform;

        public Transform Transform => _transform;

        public void Destroy() {
            gameObject.SetActive(false);
            GameObject.Destroy(gameObject);
        }
    }
}