using System;
using System.Collections.Generic;
using UnityEngine;


namespace UI.Store
{
    public class StrengthenDicePageGroupUI : MonoBehaviour
    {
        [SerializeField] private List<StrengthenDicePageUIObject> dicePageList;
        
        /// <summary>
        /// 创建一个骰面页UI
        /// </summary>
        /// <param name="index">序列索引</param>
        /// <param name="diceObjs">骰面列表</param>
        /// <param name="dicePage">父物体</param>
        /// <param name="onChooseList">选择后的事件列表</param>
        private void CreateFightDicePageColumns(int index, Transform dicePage, List<SingleDiceObj> diceObjs, List<Action<SingleDiceObj>> onChooseList)
        {
            var tmp = Instantiate(StrengthenAreaManager.Instance.dicePageColumnsTemplate, dicePage, true);
            tmp.GetComponent<StrengthenDicePageUIObject>().Init(index, diceObjs, onChooseList); //初始化
            dicePageList.Add(tmp.GetComponent<StrengthenDicePageUIObject>());
            tmp.SetActive(true);
        }

        public void Init(int index, List<List<SingleDiceObj>> dicePageList, List<List<Action<SingleDiceObj>>> onChooseGroupList)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            if (dicePageList == null || dicePageList.Count == 0)
            {
                StoreManager.Instance.m_Debug($"骰面页组合表为空");
                return;
            }
            for (int i = 0; i < dicePageList.Count; i++)
            {
                if (dicePageList[i] == null)
                {
                    StoreManager.Instance.m_Debug($"骰面页组合{nameof(gameObject)}{i}为空");
                    continue;
                }

                if (i >= 5)
                {
                    StoreManager.Instance.m_Debug($"错误,超出骰面页组合{nameof(gameObject)}5个限制");
                    break;
                }
                CreateFightDicePageColumns(i + index * 5, this.transform, dicePageList[i], onChooseGroupList[i]);
            }
        }

        /// <summary>
        /// 移除函数
        /// </summary>
        public void Remove()
        {
            Destroy(gameObject);
        }
        
    }
}
