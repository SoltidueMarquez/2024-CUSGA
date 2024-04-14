using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store
{
    public class StoreSacredUIObject : StoreDiceUIObject
    {
        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init(string id, float animTime, float scale, Action onChoose, HalidomObject halidomObject)
        {
            //大小初始化
            this.transform.localScale = new Vector3(1, 1, 1);
            //信息初始化
            var tmpData = ResourcesManager.GetHalidomUIData(id);
            nameText.text = tmpData.name;
            valueText.text = $"售价￥{tmpData.value}";
            descriptionText.text = tmpData.description;
            this.GetComponent<Image>().sprite = tmpData.sprite;
            //按钮事件绑定
            this.GetComponent<Button>().onClick.AddListener(()=>
            {
                Disable();
                DoChosenAnim(animTime, scale);//动画
                onChoose?.Invoke();
            });
        }
    }
}
