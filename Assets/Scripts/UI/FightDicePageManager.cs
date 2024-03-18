using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UI
{
    public class FightDicePageManager : MonoBehaviour
    {
        [Header("战斗骰面相关")] 
        [Tooltip("骰子页面列表")] public List<GameObject> fightDiceLists;
        [SerializeField, Tooltip("向左翻按钮")] private Button switchButtonLeft;
        [SerializeField, Tooltip("向右翻按钮")] private Button switchButtonRight;
        [SerializeField, Tooltip("页码标识文本")] private Text pageText;
        private int curPageNum;

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
            fightDiceLists[curPageNum].SetActive(false);
            curPageNum = (curPageNum - 1 >= 0) ? curPageNum - 1 : fightDiceLists.Count - 1;
            fightDiceLists[curPageNum].SetActive(true);
            pageText.text = $"{curPageNum + 1}/{fightDiceLists.Count}";
        }
        
        /// <summary>
        /// 向右切换页面
        /// </summary>
        private void SwitchRight()
        {
            fightDiceLists[curPageNum].SetActive(false);
            curPageNum = (curPageNum + 1 <= fightDiceLists.Count - 1) ? curPageNum + 1 : 0;
            fightDiceLists[curPageNum].SetActive(true);
            pageText.text = $"{curPageNum + 1}/{fightDiceLists.Count}";
        }
    }
}
