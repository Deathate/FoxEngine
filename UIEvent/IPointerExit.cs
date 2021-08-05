using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FoxUI
{
    public class IPointerExit : MonoBehaviour, IPointerExitHandler
    {
        public UnityEvent OnTouchExit;
        public void OnPointerExit(PointerEventData eventData)
        {
            OnTouchExit.Invoke();
        }
    }
}