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
        public override void Init(string id, float animTime, float scale, Action<int> onChoose, int index)
        {
            descriptionText.text = id;
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
