using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Events;

namespace com.Tanks.TanksBattle.Tank {
    public interface ITankModel : IGameEntity {
        ITankEventProvider EventProvider { get; }
    }
}