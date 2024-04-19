using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EditableDiceUIManager : MonoSingleton<EditableDiceUIManager>
    {
        [Header("动画设置")]
        [Tooltip("骰面晃动的角度")] public float shakeAngle;
        [Tooltip("骰面变大的倍数")] public float previewSize;
        [Tooltip("浮动距离")] public float flowDis;
        
        [Header("栏位设置")]
        [Tooltip("栏目判定")] public float offset;
        [Tooltip("战斗栏列表")] public List<Column> fightColumns;
        [Tooltip("背包栏列表")] public List<Column> bagColumns;
        public List<Column> allColumns;
        
        [Header("模板设置")]
        [Tooltip("所有骰子的父物体")] public Transform parent;
        [SerializeField, Tooltip("生成模板")] private GameObject template;

        [Header("战斗骰面页相关")]
        [SerializeField, Tooltip("玩家的骰子列表")] private List<LogicDice> diceList;
        [Tooltip("当前是第几号骰子")]private int _currentDiceIndex;
        [SerializeField, Tooltip("左切按钮")] private Button switchLeftBtn;
        [SerializeField, Tooltip("右切按钮")] private Button switchRightBtn;
        [SerializeField, Tooltip("当前页码")] private Text curPageText;
        [SerializeField, Tooltip("遮罩")] private GameObject mask;

        /// <summary>
        /// 设置是否可以交互
        /// </summary>
        /// <param name="flag"></param>
        public void SetActivity(bool flag)
        {
            mask.SetActive(!flag);
        }
        
        /// <summary>
        /// 获取当前页码
        /// </summary>
        /// <returns></returns>
        public int GetCurrentPage()
        {
            return _currentDiceIndex;
        }

        /// <summary>
        /// 战斗骰面页和背包UI初始化函数
        /// </summary>
        /// <param name="logicDiceList">玩家的骰子列表</param>
        /// <param name="bagDiceList">玩家的背包骰面列表</param>
        public void Init(List<LogicDice> logicDiceList, LogicDice bagDiceList)
        {
            //骰子初始化
            diceList = logicDiceList;
            //栏位初始化
            foreach (var column in fightColumns)
            {
                column.state = EditState.FightDice;
                allColumns.Add(column);
            }
            foreach (var column in bagColumns)
            {
                column.state = EditState.BagDice;
                allColumns.Add(column);
            }
            //战斗骰面页初始化
            RemoveAllFightDice();
            SwitchPage(0);//创建第0页
            _currentDiceIndex = 0;
            switchLeftBtn.onClick.AddListener(SwitchLeft);
            switchRightBtn.onClick.AddListener(SwitchRight);
            //背包初始化
            InitBagUIDice(bagDiceList.singleDiceList, bagDiceList.removeList);
        }
        
        #region 背包骰面
        /// <summary>
        /// 生成单个背包骰面函数
        /// </summary>
        /// <param name="remove"></param>
        /// <param name="singleDiceObj"></param>
        public void CreateBagUIDice(SingleDiceObj singleDiceObj, Action<SingleDiceObj> remove)
        {
            var index = UIManager.Instance.FindFirstEmptyColumn(bagColumns);
            if (index == -1) 
            {
                Debug.LogWarning("错误,背包栏位溢出");
                return;
            }
            if (bagColumns[index].bagObject != null)
            {
                Debug.LogWarning("错误，所在栏位已经有骰面存在");
                return;
            }
            var tmp = Instantiate(template, parent, true);
            tmp.transform.position = bagColumns[index].transform.position;//更改位置
            tmp.GetComponent<EditableDiceUIObject>().Init(bagColumns, offset, singleDiceObj, remove);//初始化
            tmp.SetActive(true);
        }
        /// <summary>
        /// 初始化背包骰面
        /// </summary>
        /// <param name="diceList"></param>
        /// <param name="removeList"></param>
        private void InitBagUIDice(List<SingleDiceObj> diceList, List<Action<SingleDiceObj>> removeList)
        {
            RemoveAllBagDice();
            for (int i = 0; i < diceList.Count; i++)
            {
                CreateBagUIDice(diceList[i], removeList[i]);
            }
        }

        /// <summary>
        /// 更新UI函数
        /// </summary>
        /// <param name="dice"></param>
        public void UpdateBagDiceUI(SingleDiceObj dice)
        {
            var index = FindBagDice(dice);
            if (index < 0) { return;}
            if (bagColumns[index].bagObject == null)
            {
                Debug.LogWarning("错误，所在背包栏位没有骰面存在");
                return;
            }
            var tmp = bagColumns[index].bagObject.GetComponent<EditableDiceUIObject>();
            tmp.UpdateDiceUI();
        }
        /// <summary>
        /// 移除背包骰面函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        private void RemoveBagDice(int index)
        {
            if (bagColumns[index].bagObject == null)
            {
                Debug.LogWarning("错误，所在背包栏位没有骰面存在");
                return;
            }
            var tmp = bagColumns[index].bagObject.GetComponent<EditableDiceUIObject>();
            tmp.DestroyUI(0);
        }

        private void RemoveAllBagDice()
        {
            for (int i = 0; i < bagColumns.Count; i++)
            {
                if (bagColumns[i].bagObject != null)
                {
                    RemoveBagDice(i);
                }
            }
        }
        public List<SingleDiceObj> GetBagList()
        {
            List<SingleDiceObj> bagList = new List<SingleDiceObj>();
            foreach (var column in bagColumns)
            {
                bagList.Add(column.bagObject != null ? column.bagObject.GetComponent<EditableDiceUIObject>().diceObj : null);
            }
            return bagList;
        }

        private int FindBagDice(SingleDiceObj singleDice)
        {
            var list = GetBagList();
            for (int i = 0; i < list.Count; i++)
            {
                if (singleDice == list[i])
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region 战斗骰面
        /// <summary>
        /// 生成单个战斗骰面函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        /// <param name="remove"></param>
        /// <param name="singleDiceObj"></param>
        private void CreateFightUIDice(int index, SingleDiceObj singleDiceObj, Action<SingleDiceObj> remove)
        {
            if (index >= fightColumns.Count)
            {
                Debug.LogWarning("错误，战斗栏位溢出");
                return;
            }
            if (fightColumns[index].bagObject != null)
            {
                Debug.LogWarning("错误，所在战斗栏位已经有骰面存在");
                return;
            }
            var tmp = Instantiate(template, parent, true);
            tmp.transform.position = fightColumns[index].transform.position;//更改位置
            tmp.GetComponent<EditableDiceUIObject>().Init(fightColumns, offset, singleDiceObj, remove);//初始化
            tmp.SetActive(true);
        }
        private void CreateSingleFightDice(LogicDice dice)
        {
            for (int i = 0; i < dice.singleDiceList.Count; i++)
            {
                CreateFightUIDice(i, dice.singleDiceList[i], dice.removeList[i]);
            }
        }

        /// <summary>
        /// 更新UI函数
        /// </summary>
        /// <param name="index"></param>
        public void UpdateFightDiceUI(int index)
        {
            if (fightColumns[index].bagObject == null)
            {
                Debug.LogWarning("错误，所在战斗栏位没有骰面存在");
                return;
            }
            var tmp = fightColumns[index].bagObject.GetComponent<EditableDiceUIObject>();
            tmp.UpdateDiceUI();
        }
        
        /// <summary>
        /// 切换骰子页
        /// </summary>
        /// <param name="index"></param>
        public void SwitchPage(int index)
        {
            if (index >= diceList.Count)
            {
                Debug.LogWarning("错误，不存在该骰子");
                return;
            }
            RemoveAllFightDice();
            CreateSingleFightDice(diceList[index]);
            _currentDiceIndex = index;
            curPageText.text = $"{(index + 1).ToString()}/{diceList.Count}";
        }
        private void SwitchRight()
        {
            var page = (_currentDiceIndex + 1 >= diceList.Count) ? _currentDiceIndex : _currentDiceIndex + 1;
            SwitchPage(page);
        }
        private void SwitchLeft()
        {
            var page = (_currentDiceIndex - 1 < 0) ? _currentDiceIndex : _currentDiceIndex - 1;
            SwitchPage(page);
        }
        public void SwitchToPosition(Vector2Int position)
        {
            SwitchPage(position.x);
            fightColumns[position.y].transform.GetComponent<Image>().DOColor(Color.red, 0.2f);
        }
        public void RevertMarkColumn()
        {
            foreach (var column in fightColumns)
            {
                column.transform.GetComponent<Image>().DOColor(Color.white, 0.1f);
            }
        }
        
        /// <summary>
        /// 移除背包骰面函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        private void RemoveFightDice(int index)
        {
            if (fightColumns[index].bagObject == null)
            {
                Debug.LogWarning("错误，所在栏位没有骰面存在");
                return;
            }
            var tmp = fightColumns[index].bagObject.GetComponent<EditableDiceUIObject>();
            tmp.DestroyUI(0);
        }
        private void RemoveAllFightDice()
        {
            for (int i = 0; i < fightColumns.Count; i++)
            {
                if (fightColumns[i].bagObject != null)
                {
                    RemoveFightDice(i);
                }
            }
        }

        #endregion
    }
    
    [Serializable]
    public class LogicDice
    {
        public List<SingleDiceObj> singleDiceList;
        public List<Action<SingleDiceObj>> removeList;
        public int index;
    }
}
