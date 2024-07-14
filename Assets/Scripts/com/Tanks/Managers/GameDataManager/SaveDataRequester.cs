using System;
using Utils.Components;

namespace com.Tanks.Managers.GameDataManager {
    public class SaveDataRequester {
        private readonly ILogger _logger;
        private readonly Action _handler;

        private IApplicationEventsProvider _appEventsProvider;

        public SaveDataRequester(ILogger logger, Action handler) {
            _logger = logger;
            _handler = handler;
        }

        public void SetAppEventsProvider(IApplicationEventsProvider appEventsProvider) {
            if (_appEventsProvider != null) {
                _appEventsProvider.OnAppPause -= OnAppPause;
                _appEventsProvider.OnAppFocus -= OnAppFocus;
                _appEventsProvider.OnAppQuit -= OnAppQuit;
            }

            if (appEventsProvider == null) return;
            _appEventsProvider = appEventsProvider;
            _appEventsProvider.OnAppPause += OnAppPause;
            _appEventsProvider.OnAppFocus += OnAppFocus;
            _appEventsProvider.OnAppQuit += OnAppQuit;
        }

        private void OnAppPause(bool pauseStatus) {
            if (!pauseStatus) return;
            _logger?.Log($"{GetType().Name}.{nameof(OnAppPause)}(pauseStatus={pauseStatus})");
            _handler?.Invoke();
        }

        private void OnAppFocus(bool hasFocus) {
            if (hasFocus) return;
            _logger?.Log($"{GetType().Name}.{nameof(OnAppFocus)}(hasFocus={hasFocus})");
            _handler?.Invoke();
        }

        private void OnAppQuit() {
            _logger?.Log($"{GetType().Name}.{nameof(OnAppQuit)}()");
            _handler?.Invoke();
        }
    }
}