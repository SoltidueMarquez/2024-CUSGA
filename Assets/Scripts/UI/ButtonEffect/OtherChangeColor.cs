using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ButtonEffect
{
    public class OtherChangeColor : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private List<Image> imageList;
        [SerializeField] private List<Text> textList;
        [SerializeField] private Color oldColor;
        [SerializeField] private Color newColor;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            foreach (var image in imageList)
            {
                image.color = newColor;
            }
            foreach (var text in textList)
            {
                text.color = newColor;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            foreach (var image in imageList)
            {
                image.color = oldColor;
            }
            foreach (var text in textList)
            {
                text.color = oldColor;
            }
        }
    }
}
