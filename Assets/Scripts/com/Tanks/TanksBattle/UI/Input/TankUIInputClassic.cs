using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.Tank.Movement.Input;
using UnityEngine;
using UnityEngine.UI;

namespace com.Tanks.TanksBattle.UI.Input {
    public class TankUIInputClassic : TankUIInput {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _shotButton;

        private readonly string _axisVerticalName = "Vertical";
        private readonly string _axisHorizontalName = "Horizontal";

        override public TankMovementType MovementType => TankMovementType.Classic;

        override protected void AddListeners() {
            _shotButton.onClick.AddListener(OnShotButtonClick);
        }

        override protected void RemoveListeners() {
            _shotButton.onClick.RemoveListener(OnShotButtonClick);
        }

        override protected void SimulateInput() {
            Main.Managers.InputManager.GetAxis.Simulate(_axisVerticalName, _joystick.AxisVertical);
            Main.Managers.InputManager.GetAxis.Simulate(_axisHorizontalName, _joystick.AxisHorizontal);
        }
    }
}