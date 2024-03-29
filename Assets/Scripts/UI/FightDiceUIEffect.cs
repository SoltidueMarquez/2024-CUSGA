using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI
{
    public class FightDiceUIEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [Header("UI组件")]
        [SerializeField,Tooltip("说明UI")]protected GameObject descriptionCanvas;
        [SerializeField,Tooltip("说明Text")]protected Text descriptionText;

        /// <summary>
        /// 鼠标移动函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject,BagDiceUIManager.Instance.previewSizeB);
            UIManager.Instance.DoShake(this.GetComponent<Image>(),BagDiceUIManager.Instance.shakeAngleB);
            descriptionCanvas.SetActive(true);
        }
        
        /// <summary>
        /// 鼠标移开函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.ExitPreview(gameObject);
            descriptionCanvas.SetActive(false);
        }
        
        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init(string id)
        {
            descriptionText.text = id;
        }
    }
}
