using UnityEngine;
namespace FoxEngine {
    public class FoxSingleton<T> : MonoBehaviour where T : MonoBehaviour {
        private static T m_Instance;
        public static T _i => m_Instance ? m_Instance : m_Instance = new GameObject(typeof(T).ToString() + " (Singleton)").AddComponent<T>();
    }

    public class FoxSingletonPersistent<T> : MonoBehaviour where T : MonoBehaviour {
        private static T m_Instance;
        public static T _i {
            get {
                if (m_Instance == null) {
                    var singletonObject = new GameObject(typeof(T).ToString() + " (Singleton)");
                    m_Instance = singletonObject.AddComponent<T>();
                    Object.DontDestroyOnLoad(singletonObject);
                }
                return m_Instance;
            }
        }
    }
}
