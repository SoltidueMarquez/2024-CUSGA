using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ButtonEffect
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class ConvertColor : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        public Color oldTextColor;
        public Sprite oldSprite;
        public Color newTextColor;
        public Sprite newSprite;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            this.GetComponent<Image>().sprite = newSprite;
            this.GetComponentInChildren<Text>().color = newTextColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.GetComponent<Image>().sprite = oldSprite;
            this.GetComponentInChildren<Text>().color = oldTextColor;
        }
    }
}
