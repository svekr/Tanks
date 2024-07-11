using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.Tanks.TanksBattle.UI.Input {
    public class UIClickHandler : MonoBehaviour, IPointerClickHandler {
        private Action _clickHandler;
        private Action<UIClickHandler> _clickHandlerParametrized;

        public void SetClickHandler(Action clickHandler) {
            _clickHandler = clickHandler;
        }

        public void SetClickHandler(Action<UIClickHandler> clickHandler) {
            _clickHandlerParametrized = clickHandler;
        }

        public void OnPointerClick(PointerEventData eventData) {
            _clickHandler?.Invoke();
            _clickHandlerParametrized?.Invoke(this);
        }
    }
}