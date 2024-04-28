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
        public Color newTextColor;
        public void OnPointerEnter(PointerEventData eventData)
        {
            //this.GetComponent<Image>().color = newColor;
            this.GetComponentInChildren<Text>().color = newTextColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //this.GetComponent<Image>().color = oldColor;
            this.GetComponentInChildren<Text>().color = oldTextColor;
        }
    }
}
