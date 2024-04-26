using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class IntentionUIObjectEffect : UIObjectEffects,IPointerEnterHandler,IPointerExitHandler
    {
        /// <summary>
        /// 鼠标预览
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
            descriptionCanvas.SetActive(true);
        }

        /// <summary>
        /// 退出预览
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
            descriptionCanvas.SetActive(false);
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="dice"></param>
        public void Init(SingleDiceObj dice)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            var data = ResourcesManager.GetSingleDiceUIData(dice);
            //信息文本初始化
            nameText.text = data.name;
            typeText.text = $"类型:{data.type}";
            levelText.text = $"稀有度:{data.level}";
            valueText.text = $"售价￥{data.salevalue}";
            baseValueText.text = $"基础数值{data.baseValue}";
            descriptionText.text = $"描述:{data.description}";
            this.GetComponent<Image>().sprite = data.sprite;
            idInDiceText.text = data.idInDice.ToString();
        }
       
        /// <summary>
        /// 销毁函数
        /// </summary>
        public void DoDestroy()
        {
            Destroy(gameObject);
        }
    }
}
