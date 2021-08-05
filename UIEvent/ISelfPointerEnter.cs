
using UnityEngine;
using UnityEngine.EventSystems;

namespace FoxUI {
    public class ISelfPointerEnter : MonoBehaviour, IPointerEnterHandler {
        public MyEvent OnTouchEnter;
        public void OnPointerEnter(PointerEventData eventData) {
            OnTouchEnter?.Invoke(transform);
        }
    }
}
