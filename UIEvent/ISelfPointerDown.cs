
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FoxUI {
    [System.Serializable]
    public class MyEvent : UnityEvent<Transform> { }
    public class ISelfPointerDown : MonoBehaviour, IPointerDownHandler {
        public MyEvent OnTouchDown;
        public void OnPointerDown(PointerEventData eventData) {
            OnTouchDown?.Invoke(transform);
        }
    }
}
