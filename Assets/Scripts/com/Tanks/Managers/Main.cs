public class Main {
    static public ILogger Logger { get; set; } = null;

    static public com.Tanks.Managers.Managers Managers { get; private set; }

    static Main() {
        Managers = new com.Tanks.Managers.Managers();
    }
}