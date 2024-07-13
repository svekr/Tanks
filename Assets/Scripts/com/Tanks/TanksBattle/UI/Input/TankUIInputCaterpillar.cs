using System;
using com.Tanks.TanksBattle.Tank.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace com.Tanks.TanksBattle.UI.Input {
    public class TankUIInputCaterpillar : TankUIInput {
        [SerializeField] private Joystick _joystickLeft;
        [SerializeField] private Joystick _joystickRight;
        [SerializeField] private Button _shotButtonLeft;
        [SerializeField] private Button _shotButtonRight;

        private readonly string _axisLeftName = "LeftCaterpillar";
        private readonly string _axisRightName = "RightCaterpillar";

        override public TankMovementType MovementType => TankMovementType.Caterpillar;

        override protected void Reset() {
            _joystickLeft.Reset();
            _joystickRight.Reset();
        }

        override protected void AddListeners() {
            _shotButtonLeft.onClick.AddListener(OnShotButtonClick);
            _shotButtonRight.onClick.AddListener(OnShotButtonClick);
        }

        override protected void RemoveListeners() {
            _shotButtonLeft.onClick.RemoveListener(OnShotButtonClick);
            _shotButtonRight.onClick.RemoveListener(OnShotButtonClick);
        }

        override protected void SimulateInput() {
            Main.Managers.InputManager.GetAxis.Simulate(_axisLeftName, _joystickLeft.AxisVertical);
            Main.Managers.InputManager.GetAxis.Simulate(_axisRightName, _joystickRight.AxisVertical);
        }
    }
}