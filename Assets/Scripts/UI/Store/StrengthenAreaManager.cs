using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Store
{
    /// <summary>
    /// TODO:需要考虑到背包骰面和战斗骰面的交互后，产生需要刷新的问题
    /// </summary>
    public class StrengthenAreaManager : MonoSingleton<StrengthenAreaManager>
    {
        [Tooltip("骰面模板")]public GameObject diceTemplate;
        [Tooltip("骰面页模板")]public GameObject dicePageColumnsTemplate;
        [SerializeField, Tooltip("骰面页组合模板")] private GameObject dicePageTemplate;
        [SerializeField, Tooltip("骰面页组合父物体")] private Transform strengthenContent;
        
        [SerializeField, Tooltip("背包栏位列表")] private List<Column> bagColumnList;
        
        public float animTime;
        
        /// <summary>
        /// 创建升级的所有战斗骰面UI函数，必须在全部创建完成后再对升级页面进行初始化(调用StoreUIManager.Instance.RefreshUpgradeUI()方法)
        /// </summary>
        /// <param name="index">序号索引</param>
        /// <param name="dicePageList">骰面列表的列表，传入时请保证其包含的骰面列表的个数不超过5，如果超过请切分一下再调用</param>
        /// <param name="onChooseGroupList">骰面升级函数列表的列表</param>
        public void CreateFightDicePage(int index, List<List<SingleDiceObj>> dicePageList, List<List<Action<SingleDiceObj>>> onChooseGroupList)
        {
            var tmp = Instantiate(dicePageTemplate, strengthenContent, true);
            tmp.GetComponent<StrengthenDicePageGroupUI>().Init(index, dicePageList, onChooseGroupList);
            tmp.SetActive(true);
        }

        /// <summary>
        /// 创建骰面函数
        /// </summary>
        /// <param name="index">栏位索引</param>
        /// <param name="onChoose">选择事件</param>
        /// <param name="singleDiceObj">骰面</param>
        private void CreateBagDiceUI(int index, SingleDiceObj singleDiceObj, Action<SingleDiceObj> onChoose)
        {
            SingleDiceUIData data = ResourcesManager.GetSingleDiceUIData(singleDiceObj);
            var tmp = Instantiate(diceTemplate, bagColumnList[index].transform, true);
            tmp.transform.position = bagColumnList[index].transform.position; //更改位置
            bagColumnList[index].bagObject = tmp;
            tmp.GetComponent<StrengthenDiceUIObject>()
                .Init(data, StrengthenAreaManager.Instance.animTime, onChoose, singleDiceObj); //初始化
            tmp.SetActive(true);
        }


        /*#region 测试
        private void Test()
        {
            if (_testDicePageList == null)
            {
                _testDicePageList = new List<List<SingleDiceObj>>();
                _testDicePageList.Add(testObj);
                _testDicePageList.Add(testObj);
                _testDicePageList.Add(testObj);
                _testDicePageList.Add(testObj);
                    
            }
            Debug.Log(_testDicePageList);
            CreateFightDicePage(1, _testDicePageList, null);
        }
        [SerializeField]private List<SingleDiceObj> testObj;
        public List<List<SingleDiceObj>> _testDicePageList;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Test();
            }
        }
        #endregion*/
    }
}
