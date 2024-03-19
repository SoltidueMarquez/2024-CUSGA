using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UI
{
    public class FightDicePageManager : MonoSingleton<FightDicePageManager>
    {
        [Header("战斗骰面相关")] 
        [Tooltip("骰子页面列表")] public List<GameObject> fightDiceLists;
        [Tooltip("骰子栏位列表")] public List<GameObject> columns;
        [SerializeField, Tooltip("向左翻按钮")] private Button switchButtonLeft;
        [SerializeField, Tooltip("向右翻按钮")] private Button switchButtonRight;
        [SerializeField, Tooltip("页码标识文本")] private Text pageText;
        private int curPageNum;
        private int lastMarked;

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
            if (fightDiceLists != null)
            {
                foreach (var page in fightDiceLists)
                {
                    page.SetActive(false);
                }
                fightDiceLists[0].SetActive(true);
                curPageNum = 0;
                pageText.text = $"{curPageNum + 1}/{fightDiceLists.Count}";
            }
        }

        /// <summary>
        /// 向左切换页面
        /// </summary>
        private void SwitchLeft()
        {
            SwitchPage((curPageNum - 1 >= 0) ? curPageNum - 1 : fightDiceLists.Count - 1);
        }
        
        /// <summary>
        /// 向右切换页面
        /// </summary>
        private void SwitchRight()
        {
            SwitchPage((curPageNum + 1 <= fightDiceLists.Count - 1) ? curPageNum + 1 : 0);
        }

        /// <summary>
        /// 跳转相应页面函数
        /// </summary>
        /// <param name="position">坐标位置，先页面位置再页内位置</param>
        public void SwitchToPosition(Vector2Int position)
        {
            SwitchPage(position.x);
            MarkColumn(position.y);
        }

        /// <summary>
        /// 战斗骰页面跳转函数
        /// </summary>
        /// <param name="page">页面序号</param>
        private void SwitchPage(int page)
        {
            fightDiceLists[curPageNum].SetActive(false);
            curPageNum = page;
            fightDiceLists[curPageNum].SetActive(true);
            pageText.text = $"{curPageNum + 1}/{fightDiceLists.Count}";
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
                columns[columnNum].GetComponent<Image>().color = new Color(255,0,0,0.33f);
                lastMarked = columnNum;
            }
        }
        
        /// <summary>
        /// 取消标记函数
        /// </summary>
        /// <param name="columnNum">栏位序号</param>
        private void RevertMarkColumn()
        {
            if (lastMarked >= 0 && lastMarked < columns.Count)//溢出避免
            {
                columns[lastMarked].GetComponent<Image>().color = new Color(255,255,255,0.33f);
            }
        }
    }
}
