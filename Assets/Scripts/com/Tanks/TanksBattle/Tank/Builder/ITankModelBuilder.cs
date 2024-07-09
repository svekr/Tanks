using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Contacts;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Physics;
using com.Tanks.TanksBattle.Tank.Settings;
using com.Tanks.TanksBattle.Tank.Shooting;
using com.Tanks.TanksBattle.Tank.View;

namespace com.Tanks.TanksBattle.Tank.Builder {
    public interface ITankModelBuilder {
        EntityType EntityType { get; }

        ITankModel BuildTank(string name, ITankView view, ITankSettings settings);
        ITankPhysics BuildPhysics(ITankModel tank);
        ITankContactor BuildContactor(ITankModel tank);
        ITankMovement BuildMovement(ITankPhysics physics, ITankMovementSettings settings, ITankEventProvider eventProvider);
        ITankShooting BuildShooting(ITankModel tank, ITankShootingSettings settings);
    }
}