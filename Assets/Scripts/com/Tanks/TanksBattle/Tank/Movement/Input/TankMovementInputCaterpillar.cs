namespace com.Tanks.TanksBattle.Tank.Movement.Input {
    public class TankMovementInputCaterpillar : ITankMovementInputCaterpillar {
        private readonly string _axisLeftName = "LeftCaterpillar";
        private readonly string _axisRightName = "RightCaterpillar";

        public float AxisLeft { get; private set; }
        public float AxisRight { get; private set; }

        public void StartListenInput() {
            Main.Managers.InputManager.GetAxis.AddListener(_axisLeftName, OnAxisLeft);
            Main.Managers.InputManager.GetAxis.AddListener(_axisRightName, OnAxisRight);
        }

        public void StopListenInput() {
            Main.Managers.InputManager.GetAxis.RemoveListener(_axisLeftName, OnAxisLeft);
            Main.Managers.InputManager.GetAxis.RemoveListener(_axisRightName, OnAxisRight);
        }

        private void OnAxisLeft(float value) {
            AxisLeft = value;
        }

        private void OnAxisRight(float value) {
            AxisRight = value;
        }
    }
}