using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store
{
    public class StrengthenDicePageUIObject : MonoBehaviour
    {
        [SerializeField,Tooltip("栏位列表")] private List<Column> columns;
        [SerializeField,Tooltip("栏位列表")] private Text nameText;
        
        public void Init(int index, List<SingleDiceObj> diceList, List<Action<SingleDiceObj>> onChooseList)
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
            if (diceList == null || diceList.Count == 0)
            {
                StoreManager.Instance.m_Debug($"骰面表为空");
                return;
            }
            nameText.text = (index+1).ToString();
            for (var i = 0; i < diceList.Count; i++)
            {
                if (diceList[i] == null)
                {
                    StoreManager.Instance.m_Debug($"骰面页{nameof(gameObject)}第{i+1}位为空");
                    continue;
                }
                if (i >= columns.Count)
                {
                    StoreManager.Instance.m_Debug($"错误,超出骰面页{nameof(gameObject)}位数限制");
                    break;
                }
                CreateDiceUI(i, diceList[i], onChooseList[i], index);
            }
        }

        /// <summary>
        /// 创建骰面函数
        /// </summary>
        /// <param name="index">栏位索引</param>
        /// <param name="onChoose">选择事件</param>
        /// <param name="singleDiceObj">骰面</param>
        /// <param name="pageIndex">是第几个骰子的骰面</param>
        private void CreateDiceUI(int index, SingleDiceObj singleDiceObj, Action<SingleDiceObj> onChoose,int pageIndex)
        {
            SingleDiceUIData data = ResourcesManager.GetSingleDiceUIData(singleDiceObj);
            var tmp = Instantiate(StrengthenAreaManager.Instance.diceTemplate, columns[index].transform, true);
            tmp.transform.position = columns[index].transform.position; //更改位置
            columns[index].bagObject = tmp;
            var tmpDice = tmp.GetComponent<StrengthenDiceUIObject>();
            tmpDice.Init(data, StrengthenAreaManager.Instance.animTime, onChoose, singleDiceObj); //初始化
            tmpDice.pageIndex = pageIndex;
            tmpDice.diceType = DiceType.FightDice;
            tmp.SetActive(true);
        }
    }
}
