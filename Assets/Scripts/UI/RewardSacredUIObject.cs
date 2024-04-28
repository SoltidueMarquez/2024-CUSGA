using System;
using Audio_Manager;
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
        public void Init(string id, float animTime, float scale, Action<HalidomObject> onChoose, HalidomObject halidomObject)
        {
            //信息初始化
            var tmpData = ResourcesManager.GetHalidomUIData(id);
            nameText.text = tmpData.name;
            valueText.text = $"售价￥{tmpData.salevalue}";
            descriptionText.text = tmpData.description;
            this.GetComponent<Image>().sprite = tmpData.sprite;
            //按钮事件绑定
            this.GetComponent<Button>().onClick.AddListener(()=>
            {
                Disable();
                DoChosenAnim(animTime, scale);//动画
                onChoose?.Invoke(halidomObject);
                UIManager.Instance.rewardUIManager.DisableAllSacredObject();
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayRandomSound("clickDown");
                }
            });
        }
    }
}
