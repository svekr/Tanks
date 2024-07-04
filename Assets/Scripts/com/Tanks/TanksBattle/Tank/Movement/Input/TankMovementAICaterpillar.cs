using System.Collections.Generic;
using com.Tanks.TanksBattle.Game.GameEntity;
using com.Tanks.TanksBattle.Tank.Events;
using com.Tanks.TanksBattle.Tank.Physics;
using UnityEngine;

namespace com.Tanks.TanksBattle.Tank.Movement.Input {
    public class TankMovementAICaterpillar : TankMovementAI, ITankMovementInputCaterpillar {
        public float AxisLeft { get; private set; }
        public float AxisRight { get; private set; }

        public TankMovementAICaterpillar(ITankPhysics physicsModel, ITankEventProvider eventProvider, List<IGameEntity> entities) :
            base(physicsModel, eventProvider, entities) {
        }

        override protected void SetAxes() {
            AxisLeft = Mathf.Clamp(DeltaForward + DeltaAngle, -1f, 1f);
            AxisRight = Mathf.Clamp(DeltaForward + -DeltaAngle, -1f, 1f);
        }

        override protected void ResetAxes() {
            AxisLeft = 0f;
            AxisRight = 0f;
        }
    }
}