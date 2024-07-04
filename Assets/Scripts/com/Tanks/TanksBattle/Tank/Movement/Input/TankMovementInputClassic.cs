namespace com.Tanks.TanksBattle.Tank.Movement.Input {
    public class TankMovementInputClassic : ITankMovementInputClassic {
        private readonly string _axisVerticalName = "Vertical";
        private readonly string _axisHorizontalName = "Horizontal";

        public float AxisVertical { get; private set; }
        public float AxisHorizontal { get; private set; }

        public void StartListenInput() {
            Main.Managers.InputManager.GetAxis.AddListener(_axisVerticalName, OnAxisVertical);
            Main.Managers.InputManager.GetAxis.AddListener(_axisHorizontalName, OnAxisHorizontal);
        }

        public void StopListenInput() {
            Main.Managers.InputManager.GetAxis.RemoveListener(_axisVerticalName, OnAxisVertical);
            Main.Managers.InputManager.GetAxis.RemoveListener(_axisHorizontalName, OnAxisHorizontal);
        }

        public void Destroy() {
            StopListenInput();
        }

        public bool DoUpdate(float deltaTime) {
            return true;
        }

        private void OnAxisVertical(float value) {
            AxisVertical = value;
        }

        private void OnAxisHorizontal(float value) {
            AxisHorizontal = value;
        }
    }
}