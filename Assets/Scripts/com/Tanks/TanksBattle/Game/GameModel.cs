using System.Collections.Generic;
using com.Tanks.TanksBattle.Tank;

namespace com.Tanks.TanksBattle.Game {
    public class GameModel {
        private readonly ILogger _logger;
        private readonly List<ITankModel> _tanks;

        public List<ITankModel> Tanks => _tanks;

        public GameModel(ILogger logger) {
            _logger = logger;
            _tanks = new List<ITankModel>();
        }

        public void DoUpdate(float deltaTime) {
            foreach (var tank in _tanks) {
                tank.DoUpdate(deltaTime);
            }
        }

        public void Reset() {
            foreach (var tank in _tanks) {
                tank.Destroy();
            }
            _tanks.Clear();
        }

        public void AddTank(ITankModel tank) {
            if (tank == null || _tanks.Contains(tank)) return;
            _tanks.Add(tank);
            _logger?.Log($"GameModel.AddTank({tank.Name})");
        }
    }
}