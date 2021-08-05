
using UnityEngine;
using UnityEngine.Events;

namespace FoxUI {
    public class IStart : MonoBehaviour {
        public UnityEvent act;
        private void Start() {
            act?.Invoke();
        }
    }
}
