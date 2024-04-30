using System.Collections.Generic;
using Audio_Manager;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Tutorial
{
    public enum TutorPage
    {
        Battle,
        Reward,
        Map,
        Store
    }
    public class TutorialManager : PersistentSingleton<TutorialManager>
    {
        [SerializeField, Tooltip("教程页面列表")] private List<TutorialPage> tutorialPageList;
        [SerializeField, Tooltip("教程界面")] private GameObject panel;
        [Tooltip("离开教程界面事件")]public UnityEvent onExitTutorial;
        
        [Tooltip("测试打开的教程")]public TutorPage test;

        private void Start()
        {
            onExitTutorial.AddListener(ExitUI);
        }

        /// <summary>
        /// 切换教程
        /// </summary>
        /// <param name="index"></param>
        private void SwitchPageView(int index)
        {
            foreach (var pageView in tutorialPageList)
            {
                pageView.Inactive();
            }
            tutorialPageList[index].Active();
        }
        public void EnterUI(TutorPage pageType)
        {
            panel.SetActive(true);
            SettingsManager.Instance.FreezeMap(true);//停止地图的拖动监听
            SwitchPageView((int)pageType);
        }

        private void ExitUI()
        {
            panel.SetActive(false);
            SettingsManager.Instance.FreezeMap(false);//恢复地图的拖动监听
        }

        private void Exit()
        {
            onExitTutorial.Invoke();
        }
    }
}
