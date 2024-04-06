using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class RewardSacredUIObject : RewardDiceUIObject
    {
        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init(string id, float animTime, float scale, Action<int> onChoose, int index)
        {
            //信息初始化
            var tmpData = ResourcesManager.GetHalidomUIData(id);
            descriptionText.text = $"名称:{tmpData.name}+" +
                                   $"描述:{tmpData.description}/n" +
                                   $"售价:{tmpData.value}";
            this.GetComponent<Image>().sprite = tmpData.sprite;
            //按钮事件绑定
            this.GetComponent<Button>().onClick.AddListener(()=>
            {
                Disable();
                DoChosenAnim(animTime, scale);//动画
                onChoose?.Invoke(index);
                UIManager.Instance.rewardUIManager.DisableAllSacredObject();
            });
        }
    }
}