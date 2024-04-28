using System;
using Audio_Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class ButtonsUIEffects : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        public bool ifDoScale;
        private void Start()
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayRandomSound("clickDown");
                }
            });
            if (ifDoScale)
            {
                this.GetComponent<Button>().onClick.AddListener(() =>
                {
                    this.transform.DOScale(new Vector3(1.4f, 1.2f, 1.2f), 0.1f).SetEase(Ease.OutElastic);
                });
            }
        }

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
