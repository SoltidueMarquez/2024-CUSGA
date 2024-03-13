using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SacredObjectsUIEffects : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField,Tooltip("说明UI")]private GameObject descriptionCanvas;
        [SerializeField,Tooltip("说明Text")]private Text descriptionText;
        [Multiline(4)]
        [SerializeField, Tooltip("描述")] private string desc;

        private void Start()
        {
            descriptionText.text = desc;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject,new Vector3(1.2f,1.2f,1.2f));
            UIManager.Instance.DoShake(this.GetComponent<Image>(),5);
            descriptionCanvas.SetActive(true);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.ExitPreview(this.gameObject,new Vector3(1f,1f,1f));
            descriptionCanvas.SetActive(false);
        }
    }
}
