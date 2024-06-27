using System;
using com.Tanks.TanksBattle.Tank.Settings;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.Tanks.TanksBattle.Tank.Builder {
    abstract public class TankBuilder {
        abstract protected ITankModelBuilder ModelBuilder { get; }

        public ITankModel BuildTank(TankConfig config, Transform container) {
            if (config == null) {
                throw new ArgumentNullException();
            }
            if (config.TankView == null) {
                throw new ArgumentException("Tank view prefab can not be null");
            }
            var view = Object.Instantiate(config.TankView, container);
            var name = GetName(config);
            view.name = name;
            return ModelBuilder.BuildTank(name, view, config);
        }

        virtual protected string GetName(TankConfig config) {
            return config.TankView.name;
        }
    }
}