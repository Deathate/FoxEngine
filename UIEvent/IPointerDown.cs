using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FoxUI {
    public class IPointerDown : MonoBehaviour, IPointerDownHandler {
        public UnityEvent OnTouchDown;
        public void OnPointerDown(PointerEventData eventData) {
            OnTouchDown?.Invoke();
        }
    }
}
