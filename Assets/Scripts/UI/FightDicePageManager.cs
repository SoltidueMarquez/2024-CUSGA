using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class FightDicePageManager : MonoSingleton<FightDicePageManager>
    {
        [FormerlySerializedAs("fightDiceLists")]
        [Header("战斗骰面相关")] 
        [Tooltip("骰子页面列表")] public List<GameObject> fightDicePageLists;
        [Tooltip("骰子栏位列表")] public List<GameObject> columns;
        [SerializeField, Tooltip("向左翻按钮")] private Button switchButtonLeft;
        [SerializeField, Tooltip("向右翻按钮")] private Button switchButtonRight;
        [SerializeField, Tooltip("页码标识文本")] private Text pageText;
        [SerializeField, Tooltip("页面模板")] private GameObject pageTemplate;
        [SerializeField, Tooltip("骰子模板")] private GameObject diceTemplate;
        [SerializeField, Tooltip("父物体")] private Transform parent;

        private int curPageNum;
        private int lastMarked;

        /// <summary>
        /// 创建战斗骰子页
        /// </summary>
        /// <param name="name"></param>
        /// <param name="diceObjs"></param>
        public void CreatePageUI(string name, List<SingleDiceObj> diceObjs)
        {
            var tmp = Instantiate(pageTemplate, parent, true);
            tmp.transform.position = parent.position;
            tmp.GetComponent<FightDicePageUIEffect>().Init(name);//初始化页面
            tmp.SetActive(true);
            if (diceObjs.Count == 0)
            {
                Debug.Log("没有骰子列表");
            }
            else
            {
                for (int i = 0; i < diceObjs.Count; i++)//创建骰面
                {
                    CreateDiceUI(diceObjs[i], diceObjs[i].positionInDice, tmp.transform);
                }
            }
            fightDicePageLists.Add(tmp);
            InitPageVisibility();
        }

        /// <summary>
        /// 创建战斗骰函数
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="index">栏位索引</param>
        /// <param name="page">页面</param>
        public void CreateDiceUI(SingleDiceObj data, int index, Transform page)
        {
            var tmp = Instantiate(diceTemplate, page, true);
            tmp.transform.position = columns[index].transform.position;//更改位置
            tmp.GetComponent<FightDiceUIEffect>().Init(data);//初始化
            tmp.SetActive(true);
        }
        
        private void Start()
        {
            InitPageVisibility();
            switchButtonLeft.onClick.AddListener(SwitchLeft);
            switchButtonRight.onClick.AddListener(SwitchRight);
        }

        /// <summary>
        /// 初始化骰子页面，只有第一个可见
        /// </summary>
        private void InitPageVisibility()
        {
            if (fightDicePageLists.Count != 0)
            {
                foreach (var page in fightDicePageLists)
                {
                    page.SetActive(false);
                }
                fightDicePageLists[0].SetActive(true);
                curPageNum = 0;
                pageText.text = $"{curPageNum + 1}/{fightDicePageLists.Count}";
            }
        }

        /// <summary>
        /// 向左切换页面
        /// </summary>
        private void SwitchLeft()
        {
            SwitchPage((curPageNum - 1 >= 0) ? curPageNum - 1 : fightDicePageLists.Count - 1);
        }
        
        /// <summary>
        /// 向右切换页面
        /// </summary>
        private void SwitchRight()
        {
            SwitchPage((curPageNum + 1 <= fightDicePageLists.Count - 1) ? curPageNum + 1 : 0);
        }

        /// <summary>
        /// 跳转相应页面函数
        /// </summary>
        /// <param name="position">坐标位置，先页面位置再页内位置</param>
        public void SwitchToPosition(Vector2Int position)
        {
            if (fightDicePageLists.Count < position.x)
            {
                Debug.Log("页面不存在");
                return;
            }
            if (columns.Count < position.y)
            {
                Debug.Log("栏位不存在骰面");
                return;
            }
            SwitchPage(position.x);
            MarkColumn(position.y);
        }

        /// <summary>
        /// 战斗骰页面跳转函数
        /// </summary>
        /// <param name="page">页面序号</param>
        private void SwitchPage(int page)
        {
            if (fightDicePageLists.Count == 0)
            {
                Debug.Log("没有页面存在");
                return;
            }
            fightDicePageLists[curPageNum].SetActive(false);
            curPageNum = page;
            fightDicePageLists[curPageNum].SetActive(true);
            pageText.text = $"{curPageNum + 1}/{fightDicePageLists.Count}";
        }

        /// <summary>
        /// 标记对应的骰子栏位
        /// </summary>
        /// <param name="columnNum">栏位序号</param>
        private void MarkColumn(int columnNum)
        {
            RevertMarkColumn();//取消上一次的标记
            if (columnNum >= 0 && columnNum < columns.Count)//溢出避免
            {
                columns[columnNum].GetComponent<Image>().color = new Color(255,0,0,1f);
                lastMarked = columnNum;
            }
        }
        
        /// <summary>
        /// 取消标记函数
        /// </summary>
        /// <param name="columnNum">栏位序号</param>
        public void RevertMarkColumn()
        {
            if (lastMarked >= 0 && lastMarked < columns.Count)//溢出避免
            {
                columns[lastMarked].GetComponent<Image>().color = new Color(255,255,255,1f);
            }
        }
    }
}
