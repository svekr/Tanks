using System.Collections.Generic;

namespace Utils.Updatable {
    public class Updater : IUpdater {
        private readonly List<IUpdatable> _listToUpdate = new();
        private readonly Queue<IUpdatable> _queueToAdd = new();
        private readonly Queue<IUpdatable> _queueToRemove = new();

        private bool _isLocked;

        ~Updater() {
            Clear();
        }

        public bool DoUpdate(float deltaTime) {
            _isLocked = true;
            for (var i = 0; i < _listToUpdate.Count; i++) {
                if (_listToUpdate[i].DoUpdate(deltaTime)) continue;
                RemoveUpdatable(_listToUpdate[i]);
            }
            _isLocked = false;

            while (_queueToRemove.TryDequeue(out var updatable)) {
                RemoveUpdatable(updatable);
            }

            while (_queueToAdd.TryDequeue(out var updatable)) {
                AddUpdatable(updatable);
            }

            return true;
        }

        public void AddUpdatable(IUpdatable updatable) {
            if (updatable == null) return;
            if (_listToUpdate.Contains(updatable)) return;

            if (_isLocked) {
                _queueToAdd.Enqueue(updatable);
                return;
            }

            _listToUpdate.Add(updatable);
        }

        public void RemoveUpdatable(IUpdatable updatable) {
            if (updatable == null) return;
            if (_listToUpdate.Contains(updatable)) return;

            if (_isLocked) {
                _queueToRemove.Enqueue(updatable);
                return;
            }

            _listToUpdate.Remove(updatable);
        }

        public void Clear() {
            _listToUpdate.Clear();
            _queueToAdd.Clear();
            _queueToRemove.Clear();
        }
    }
}