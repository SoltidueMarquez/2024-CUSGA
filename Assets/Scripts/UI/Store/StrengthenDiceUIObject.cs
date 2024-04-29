using System;
using Audio_Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Store
{
    /// <summary>
    /// 继承自FightDiceUIEffect
    /// </summary>
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class StrengthenDiceUIObject : FightDiceUIEffect
    {

        public int pageIndex;
        public DiceType diceType;
        private SingleDiceObj _diceObj;

        /// <summary>
        /// 鼠标移动函数
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject, StoreAreaUIManager.Instance.previewSize,
                StoreAreaUIManager.Instance.previewSize);
            UIManager.Instance.DoShake(this.GetComponent<Image>(),StoreAreaUIManager.Instance.shakeAngle);
            descriptionCanvas.SetActive(true);
        }
        
        /// <summary>
        /// 鼠标移开函数
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.ExitPreview(gameObject);
            descriptionCanvas.SetActive(false);
        }
        
        public void Init(SingleDiceUIData data, float animTime, Action<SingleDiceObj> onChoose, SingleDiceObj singleDiceObj)
        {
            _diceObj = singleDiceObj;
            //大小初始化
            this.transform.localScale = new Vector3(1, 1, 1);
            //信息文本初始化
            nameText.text = data.name;
            typeText.text = $"类型:{data.type}";
            levelText.text = $"稀有度:{data.level}";
            baseValueText.text = $"骰面预测数值{(int)(data.baseValue * (1 + (float)data.idInDice / 10))}";
            descriptionText.text = $"描述:{data.description}";
            idInDiceText.text = data.idInDice.ToString();
            this.GetComponent<Image>().sprite = data.sprite;
            //按钮事件绑定
            this.GetComponent<Button>().onClick.AddListener(()=>
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayRandomSound("clickDown");
                }
                onChoose?.Invoke(singleDiceObj);
                //根据SingleDiceObj引用更新UI，需要放在执行强化逻辑之后
                UpdateDiceUI();
                UpdatePlayerResourceDiceUI();
            });
        }
        
        /// <summary>
        /// 根据SingleDiceObj引用更新UI
        /// </summary>
        private void UpdateDiceUI()
        {
            var data = ResourcesManager.GetSingleDiceUIData(_diceObj);
            //大小初始化
            this.transform.localScale = new Vector3(1, 1, 1);
            //信息文本初始化
            nameText.text = data.name;
            typeText.text = $"类型:{data.type}";
            levelText.text = $"稀有度:{data.level}";
            baseValueText.text = $"骰面预测数值{(int)(data.baseValue * (1 + (float)data.idInDice / 10))}";
            descriptionText.text = $"描述:{data.description}";
            idInDiceText.text = data.idInDice.ToString();
            this.GetComponent<Image>().sprite = data.sprite;
        }
        /// <summary>
        /// 更新玩家资源区域的UI
        /// </summary>
        private void UpdatePlayerResourceDiceUI()
        {
            switch (diceType)
            {
                case DiceType.FightDice:
                    EditableDiceUIManager.Instance.SwitchPage(pageIndex);
                    EditableDiceUIManager.Instance.UpdateFightDiceUI(_diceObj.positionInDice);
                    break;
                case DiceType.BagDice:
                    EditableDiceUIManager.Instance.UpdateBagDiceUI(_diceObj);
                    break;
                default:
                    break;
            }
        }
    }
}
