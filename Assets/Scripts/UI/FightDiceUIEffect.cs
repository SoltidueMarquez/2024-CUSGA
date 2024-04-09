using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class FightDiceUIEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [Header("UI组件")]
        [SerializeField,Tooltip("说明UI")]protected GameObject descriptionCanvas;
        [SerializeField,Tooltip("说明Text")]protected Text descriptionText;
        [SerializeField,Tooltip("名称Text")]protected Text nameText;
        [SerializeField,Tooltip("类型Text")]protected Text typeText;
        [SerializeField, Tooltip("稀有度Text")] protected Text levelText;
        [SerializeField,Tooltip("售价Text")]protected Text valueText;
        [SerializeField,Tooltip("基础数值Text")]protected Text baseValueText;
        [SerializeField,Tooltip("点数Text")]protected Text idInDiceText;

        /// <summary>
        /// 鼠标移动函数
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject,BagDiceUIManager.Instance.previewSizeB);
            UIManager.Instance.DoShake(this.GetComponent<Image>(),BagDiceUIManager.Instance.shakeAngleB);
            descriptionCanvas.SetActive(true);
        }
        
        /// <summary>
        /// 鼠标移开函数
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.ExitPreview(gameObject);
            descriptionCanvas.SetActive(false);
        }
        
        /// <summary>
        /// 初始化函数
        /// </summary>
        public virtual void Init(SingleDiceUIData data)
        {
            //信息文本初始化
            nameText.text = data.name;
            typeText.text = $"类型:{data.type}";
            levelText.text = $"稀有度:{data.level}";
            valueText.text = $"售价￥{data.value}";
            baseValueText.text = $"基础数值{data.baseValue}";
            descriptionText.text = $"描述:{data.description}";
            idInDiceText.text = data.idInDice.ToString();
            this.GetComponent<Image>().sprite = data.sprite;
        }
    }
}
