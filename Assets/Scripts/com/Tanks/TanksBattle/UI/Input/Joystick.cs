using UnityEngine;
using UnityEngine.EventSystems;

namespace com.Tanks.TanksBattle.UI.Input {
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private bool _verticalBlocked;
        [SerializeField] private bool _horizontalBlocked;

        private float _inputX;
        private float _inputY;
        private Vector2 _backgroundPosition;
        private Vector2 _backgroundHalfSize;
        private Vector2 _backgroundHalfSizeScaled;

        public float AxisVertical => _inputY;
        public float AxisHorizontal => _inputX;

        public void OnPointerDown(PointerEventData eventData) {
            _backgroundPosition = RectTransformUtility.WorldToScreenPoint(null, _background.position);
            _backgroundHalfSize = (_background.sizeDelta - _handle.sizeDelta) / 2;
            _backgroundHalfSizeScaled = _backgroundHalfSize * _canvas.scaleFactor;
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData) {
            var input = (eventData.position - _backgroundPosition) / _backgroundHalfSizeScaled;
            if (input.magnitude > 1f) {
                input = input.normalized;
            }
            if (_verticalBlocked) {
                input.x = 0;
            }
            if (_horizontalBlocked) {
                input.y = 0;
            }
            _handle.anchoredPosition = input * _backgroundHalfSize;
            _inputX = input.x;
            _inputY = input.y;
        }

        public void OnPointerUp(PointerEventData eventData) {
            _inputX = 0;
            _inputY = 0;
            _handle.anchoredPosition = Vector2.zero;
        }

        private void Start() {
            _inputX = 0;
            _inputY = 0;
        }
    }
}