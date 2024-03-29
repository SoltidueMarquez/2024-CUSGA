using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ButtonsUIEffects : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            this.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
        }
    }
}
