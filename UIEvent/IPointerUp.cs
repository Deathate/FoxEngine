using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FoxUI
{
    [RequireComponent(typeof(IPointerDown))]
    public class IPointerUp : MonoBehaviour, IPointerUpHandler
    {
        public UnityEvent OnTouchUp;
        public void OnPointerUp(PointerEventData eventData)
        {
            OnTouchUp.Invoke();
        }

    }
}
