using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.View;

namespace com.Tanks.TanksBattle.Tank {
    public interface ITankModel : IGameEntity {
        ITankEventProvider EventProvider { get; }
        ITankView TankView { get; }
    }
}