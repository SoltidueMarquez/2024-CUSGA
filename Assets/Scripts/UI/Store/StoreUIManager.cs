using System;
using UnityEngine;

namespace UI.Store
{
    public class StoreUIManager : MonoSingleton<StoreUIManager>
    {
        public RectTransform strengthenBg;
        public GameObject strengthenCanvas;
        public PageView strengthenPage;
        
        /// <summary>
        /// 进入商店UI的动画
        /// </summary>
        public void EnterStoreUI()
        {
            StoreManager.Instance.m_Debug("进入动画");
        }
        
        /// <summary>
        /// 退出商店UI的动画
        /// </summary>
        public void ExitStoreUI()
        {
            StoreManager.Instance.m_Debug("离开动画");
        }

        public void EnterUpgradeUI()
        {
            strengthenPage.Init(strengthenBg);
            strengthenCanvas.SetActive(true);
        }

        public void RefreshUpgradeUI()
        {
            strengthenPage.Init(strengthenBg);
        }
        
        public void ExitUpgradeUI()
        {
            strengthenCanvas.SetActive(false);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StoreManager.Instance.OnEnterStore.Invoke();
            }
        }
    }
}
