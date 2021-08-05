
using UnityEngine;
using UnityEngine.EventSystems;

namespace FoxUI {
    public class ISeffPointerExit : MonoBehaviour, IPointerExitHandler {
        public MyEvent OnTouchExit;
        public void OnPointerExit(PointerEventData eventData) {
            OnTouchExit?.Invoke(transform);
        }
    }
}
