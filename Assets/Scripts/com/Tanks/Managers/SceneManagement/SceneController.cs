using UnityEngine;

namespace com.Tanks.Managers.SceneManagement {
    abstract public class SceneController : MonoBehaviour {
        [SerializeField] private string _location;

        public string Location => _location;

        public ILogger Logger { get; protected set; } = null;

        protected void Awake() {
            InitLogger();
            Logger?.Log($"Scene '{Location}' Awake");
            AwakeHandler();
        }

        protected void Start() {
            Logger?.Log($"Scene '{Location}' Start");
            StartHandler();
        }

        protected void OnDestroy() {
            Logger?.Log($"Scene '{Location}' Destroy");
            DestroyHandler();
        }

        abstract protected void InitLogger();

        virtual protected void AwakeHandler() {

        }

        virtual protected void StartHandler() {

        }

        virtual protected void DestroyHandler() {

        }
    }
}