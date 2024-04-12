using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace UI.Store
{
    public class StoreUIManager : MonoSingleton<StoreUIManager>
    {
        public RectTransform strengthenBg;
        public GameObject strengthenCanvas;
        
        [SerializeField, Tooltip("动画时间")] private float animTime;
        [SerializeField, Tooltip("商店区域")] private GameObject storeArea;
        [SerializeField, Tooltip("投掷区域")] private GameObject rollArea;
        
        [SerializeField, Tooltip("商店出现位置")] private Transform appearTransformPosition;
        [SerializeField, Tooltip("商店离开位置")] private Transform disappearTransformPosition;
        public PageView strengthenPage;

        /// <summary>
        /// 进入商店UI的动画
        /// </summary>
        public void EnterStoreUI()
        {
            storeArea.transform.DOMoveX(appearTransformPosition.position.x, animTime);
        }
        
        /// <summary>
        /// 退出商店UI的动画
        /// </summary>
        public void ExitStoreUI()
        {
            storeArea.transform.DOMoveX(disappearTransformPosition.position.x, animTime);
        }

        public void EnterUpgradeUI()
        {
            strengthenPage.Init(strengthenBg);
            strengthenCanvas.SetActive(true);
            strengthenCanvas.transform.DOMoveX(appearTransformPosition.position.x, animTime);
        }

        public void RefreshUpgradeUI()
        {
            strengthenPage.Init(strengthenBg);
        }
        
        public void ExitUpgradeUI()
        {
            strengthenCanvas.transform.DOMoveX(disappearTransformPosition.position.x, animTime);
            StartCoroutine(LateInactive(animTime,strengthenCanvas));
        }

        IEnumerator LateInactive(float time, GameObject objectX)
        {
            yield return new WaitForSeconds(time);
            objectX.SetActive(false);
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
