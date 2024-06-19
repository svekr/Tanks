using com.Tanks.TanksBattle.Controllers.Tank.Input;
using com.Tanks.TanksBattle.Controllers.Tank.Movement;
using com.Tanks.TanksBattle.Controllers.Tank.Physics;
using com.Tanks.TanksBattle.Controllers.Tank.View;

namespace com.Tanks.TanksBattle.Controllers.Tank.Model {
    public interface ITankModel {
        ITankView View { get; }
        ITankPhysics Physics { get; }
        ITankInput Input { get; }
        ATankMovement Movement { get; }
    }
}