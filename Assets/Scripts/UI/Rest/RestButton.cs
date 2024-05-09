using Rest;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Rest
{
    public class RestButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private RestType type;
        public void OnPointerEnter(PointerEventData eventData)
        {
            RestManager.Instance.ChangeDesc(type);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RestManager.Instance.HideDesc();
        }
    }
}
