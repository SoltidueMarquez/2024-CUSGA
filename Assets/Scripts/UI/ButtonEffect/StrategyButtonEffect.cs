using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ButtonEffect
{
    [RequireComponent(typeof(Button))]
    public class StrategyButtonEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        

        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}
