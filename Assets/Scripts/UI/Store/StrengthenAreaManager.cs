using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store
{
    public enum DiceType
    {
        FightDice,
        BagDice
    }
    /// <summary>
    /// TODO:需要考虑到背包骰面和战斗骰面的交互后，产生需要刷新的问题(或者直接将左边的UI全部屏蔽掉)
    /// </summary>
    public class StrengthenAreaManager : MonoSingleton<StrengthenAreaManager>
    {
        [Tooltip("骰面模板")] public GameObject diceTemplate;
        [Tooltip("骰面页模板")] public GameObject dicePageColumnsTemplate;
        [SerializeField, Tooltip("骰面页组合模板")] private GameObject dicePageTemplate;
        [SerializeField, Tooltip("骰面页组合父物体")] private Transform strengthenContent;
        private List<StrengthenDicePageGroupUI> _groupList;
        [SerializeField, Tooltip("背包栏位列表")] private List<Column> bagColumnList;
        [SerializeField, Tooltip("离开按钮")] private Button exitButton;
        public float animTime;
        public Text priceText;

        [SerializeField, Tooltip("当前是第几个骰子")] private int currentPageIndex;
        [SerializeField, Tooltip("当前是哪种骰子")] private DiceType currentDiceType;

        
        private void Start()
        {
            exitButton.onClick.AddListener(ExitUpgradeUI);
            StoreManager.Instance.OnUpgradeSuccess.AddListener(UpdateFightDiceUI);//添加更新UI事件
        }

        /// <summary>
        /// 退出UI函数
        /// </summary>
        private void ExitUpgradeUI()
        {
            StoreUIManager.Instance.ExitUpgradeUI();
            RemoveAllDicePage();
            RemoveAllBagDiceUI();
            StoreAreaUIManager.Instance.SetButton(); //设置强化按钮可以交互
        }
        
        /// <summary>
        /// 更新当前的选择信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="diceType"></param>
        public void UpdateCurrentDice(int pageIndex, DiceType diceType)
        {
            currentPageIndex = pageIndex;
            currentDiceType = diceType;
        }
        private void UpdateFightDiceUI(UpgradeInfo upgrade)
        {
            switch (currentDiceType)
            {
                case DiceType.FightDice:
                    EditableDiceUIManager.Instance.SwitchPage(currentPageIndex);
                    _groupList[currentPageIndex / 5].UpdateDiceUI(upgrade, currentPageIndex);
                    EditableDiceUIManager.Instance.UpdateFightDiceUI(upgrade, upgrade._singleDiceObj.positionInDice);
                    break;
                case DiceType.BagDice:
                    UpdateDiceUI(upgrade, currentPageIndex);
                    EditableDiceUIManager.Instance.UpdateBagDiceUI(upgrade, currentPageIndex);
                    break;
            }
        }

        #region 战斗骰面
        /// <summary>
        /// 划分函数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<List<T>> DivideList<T>(List<T> list)
        {
            List<List<T>> ansList = new List<List<T>>();

            int startIndex = 0;
            while (startIndex < list.Count)
            {
                List<T> sublist = new List<T>();
                int endIndex = Mathf.Min(startIndex + 5, list.Count);
                for (int i = startIndex; i < endIndex; i++)
                {
                    sublist.Add(list[i]);
                }
                ansList.Add(sublist);
                startIndex = endIndex;
            }

            return ansList;
        }

        /// <summary>
        /// 创建升级的所有战斗骰面UI函数
        /// </summary>
        /// <param name="dicePageList"></param>
        /// <param name="onChooseGroupList"></param>
        public void CreateFightDicePage(List<List<SingleDiceObj>> dicePageList, List<List<Action<SingleDiceObj>>> onChooseGroupList)
        {
            var tmpDiceList = DivideList(dicePageList);
            var tmpActionList = DivideList(onChooseGroupList);

            for (int i = 0; i < tmpDiceList.Count; i++)
            {
                CreateFightDicePage(i, tmpDiceList[i], tmpActionList[i]);
            }
        }

        /// <summary>
        /// 创建升级的所有战斗骰面UI函数，必须在全部创建完成后再对升级页面进行初始化(调用StoreUIManager.Instance.RefreshUpgradeUI()方法)
        /// </summary>
        /// <param name="index">序号索引</param>
        /// <param name="dicePageList">骰面列表的列表，传入时请保证其包含的骰面列表的个数不超过5，如果超过请切分一下再调用</param>
        /// <param name="onChooseGroupList">骰面升级函数列表的列表</param>
        private void CreateFightDicePage(int index, List<List<SingleDiceObj>> dicePageList, List<List<Action<SingleDiceObj>>> onChooseGroupList)
        {
            var tmp = Instantiate(dicePageTemplate, strengthenContent, true);
            var tmpGroup = tmp.GetComponent<StrengthenDicePageGroupUI>();
            tmpGroup.Init(index, dicePageList, onChooseGroupList);
            if (_groupList == null) { _groupList = new List<StrengthenDicePageGroupUI>(); }
            _groupList.Add(tmpGroup);
            tmp.SetActive(true);
        }

        /// <summary>
        /// 销毁全部战斗骰面页UI
        /// </summary>
        public void RemoveAllDicePage()
        {
            foreach (var group in _groupList)
            {
                group.Remove();
            }
            _groupList.Clear();
        }
        #endregion

        #region 背包骰面
        /// <summary>
        /// 创建背包骰面函数
        /// </summary>
        /// <param name="index">栏位索引</param>
        /// <param name="onChoose">选择事件</param>
        /// <param name="singleDiceObj">骰面</param>
        public void CreateBagDiceUI(int index, SingleDiceObj singleDiceObj, Action<SingleDiceObj> onChoose)
        {
            SingleDiceUIData data = ResourcesManager.GetSingleDiceUIData(singleDiceObj);
            var tmp = Instantiate(diceTemplate, bagColumnList[index].transform, true);
            tmp.transform.position = bagColumnList[index].transform.position; //更改位置
            bagColumnList[index].bagObject = tmp;
            var tmpDice = tmp.GetComponent<StrengthenDiceUIObject>();
            tmpDice.Init(data, StrengthenAreaManager.Instance.animTime, onChoose, singleDiceObj); //初始化
            tmpDice.diceType = DiceType.BagDice;
            tmpDice.pageIndex = index;
            tmp.SetActive(true);
        }

        /// <summary>
        /// 移除对应背包骰面的函数
        /// </summary>
        /// <param name="index">栏位索引</param>
        public void RemoveBagDiceUI(int index)
        {
            if (bagColumnList[index].bagObject == null) { return; }
            Destroy(bagColumnList[index].bagObject);
            bagColumnList[index].bagObject = null;
        }
        private void RemoveAllBagDiceUI()
        {
            for (int i = 0; i < bagColumnList.Count; i++)
            {
                RemoveBagDiceUI(i);
            }
        }

        private void UpdateDiceUI(UpgradeInfo upgrade,int index)
        {
            var singleDiceObj = upgrade._singleDiceObj;
            var tmpDice = bagColumnList[index].bagObject.GetComponent<StrengthenDiceUIObject>();
            if (tmpDice != null)
            {
                tmpDice.UpdateDiceUI(singleDiceObj);
            }
        }
        #endregion

        public void RefreshUpgradeText(int value)
        {
            priceText.text = "强化价格：￥" + value.ToString();
        }

        #region 测试
        /*private void Test()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_testDicePageList == null)
                {
                    _testDicePageList = new List<List<SingleDiceObj>>();
                    _testDicePageList.Add(testObj);
                    _testDicePageList.Add(testObj);
                    _testDicePageList.Add(testObj);
                    _testDicePageList.Add(testObj);
                    _testDicePageList.Add(testObj);
                    _testDicePageList.Add(testObj);
                    _testDicePageList.Add(testObj);
                    _testDicePageList.Add(testObj);
                    
                }
                Debug.Log(_testDicePageList);
                CreateFightDicePage( _testDicePageList, null);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                RemoveAllDicePage();
            }
        }
        [SerializeField]private List<SingleDiceObj> testObj;
        public List<List<SingleDiceObj>> _testDicePageList;
        private void Update()
        {
            Test();
        }*/
        #endregion
    }
}
