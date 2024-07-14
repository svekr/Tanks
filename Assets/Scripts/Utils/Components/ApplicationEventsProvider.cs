using System;
using UnityEngine;

namespace Utils.Components {
    public class ApplicationEventsProvider : MonoBehaviour, IApplicationEventsProvider {
        public event Action<bool> OnAppFocus;
        public event Action<bool> OnAppPause;
        public event Action OnAppQuit;

        private void OnApplicationFocus(bool hasFocus) {
            OnAppFocus?.Invoke(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus) {
            OnAppPause?.Invoke(pauseStatus);
        }

        private void OnApplicationQuit() {
            OnAppQuit?.Invoke();
        }

        private void OnDestroy() {
            OnAppFocus = null;
            OnAppPause = null;
            OnAppQuit = null;
        }
    }

    public interface IApplicationEventsProvider {
        event Action<bool> OnAppFocus;
        event Action<bool> OnAppPause;
        event Action OnAppQuit;
    }
}