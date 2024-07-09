using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Game.Settings;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Physics;

namespace com.Tanks.TanksBattle.Tank.Movement.Input {
    public class TankMovementAIClassic : TankMovementAI, ITankMovementInputClassic {
        public TankMovementAIClassic(TankAIMovementSettings aiSettings, ITankPhysics physicsModel,
            ITankEventProvider eventProvider, List<IGameEntity> entities) :
            base(aiSettings, physicsModel, eventProvider, entities) {
        }

        public float AxisVertical { get; private set; }
        public float AxisHorizontal { get; private set; }

        override protected void SetAxes() {
            AxisVertical = DeltaForward;
            AxisHorizontal = DeltaAngle;
        }

        override protected void ResetAxes() {
            AxisVertical = 0f;
            AxisHorizontal = 0f;
        }
    }
}