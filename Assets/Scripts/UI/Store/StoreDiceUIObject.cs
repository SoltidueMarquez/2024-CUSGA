using System;
using Audio_Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Store
{
    /// <summary>
    /// 商店骰面UI物体
    /// </summary>
    public class StoreDiceUIObject : RewardDiceUIObject
    {
        protected float _animTime;
        protected float _scale;
        
        public void Init( float animTime, float scale, Action<SingleDiceObj> onChoose, SingleDiceObj singleDiceObj)
        {
            var data = ResourcesManager.GetSingleDiceUIData(singleDiceObj);
            //大小初始化
            this.transform.localScale = new Vector3(1, 1, 1);
            _scale = scale;
            _animTime = animTime;
            //信息文本初始化
            nameText.text = data.name;
            typeText.text = $"类型:{data.type}";
            levelText.text = $"稀有度:{data.level}";
            baseValueText.text = $"骰面预测数值{(int)(data.baseValue * (1 + (float)data.idInDice / 10))}";
            descriptionText.text = $"{data.description}";
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
            });

            var parent = transform.parent.GetComponent<ProductDice>();
            parent.OnBuySuccess.AddListener(Disable);
            parent.OnBuySuccess.AddListener(DoChosenAnim);
            parent.OnBuySuccess.AddListener(BusinessmenTipManager.Instance.ShowTip);//增加提示
            parent.OnBuyFail.AddListener(BusinessmenTipManager.Instance.ShowTip);//增加提示
        }
        
        private void RemoveListener()
        {
            var parent = transform.parent.GetComponent<ProductDice>();
            parent.OnBuySuccess.RemoveListener(Disable);
            parent.OnBuySuccess.RemoveListener(DoChosenAnim);
            parent.OnBuySuccess.RemoveListener(BusinessmenTipManager.Instance.ShowTip);//增加提示
            parent.OnBuyFail.RemoveListener(BusinessmenTipManager.Instance.ShowTip);//增加提示
        }
        
        private void DoChosenAnim()
        {
            DoChosenAnim(_animTime, _scale);
            RemoveListener();
        }
        
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

    }
}
