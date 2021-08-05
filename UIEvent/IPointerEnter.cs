using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FoxUI
{
    public class IPointerEnter : MonoBehaviour, IPointerEnterHandler
    {
        public UnityEvent OnTouching;
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnTouching.Invoke();
        }
    }
}
