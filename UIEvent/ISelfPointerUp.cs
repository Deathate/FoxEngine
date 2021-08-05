
using UnityEngine;
using UnityEngine.EventSystems;

namespace FoxUI {
    public class ISelfPointerUp : MonoBehaviour, IPointerUpHandler {
        public MyEvent OnTouchUp;
        public void OnPointerUp(PointerEventData eventData) {
            OnTouchUp?.Invoke(transform);
        }
    }
}
