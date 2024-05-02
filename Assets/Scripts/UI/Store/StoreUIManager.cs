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

        [SerializeField, Tooltip("动画时间")] public float animTime;
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
            SettingsManager.Instance.FreezeMap(true);//停止地图的拖动监听
            storeArea.transform.DOMoveX(appearTransformPosition.position.x, animTime);
            StoreAreaUIManager.Instance.SetButton();
        }

        /// <summary>
        /// 退出商店UI的动画
        /// </summary>
        public void ExitStoreUI()
        {
            SettingsManager.Instance.FreezeMap(false);//停止地图的拖动监听
            storeArea.transform.DOMoveX(disappearTransformPosition.position.x, animTime);
        }

        public void EnterUpgradeUI()
        {
            SettingsManager.Instance.FreezeMap(true);//停止地图的拖动监听
            EditableDiceUIManager.Instance.SetActivity(false);//设置侧边物UI不可交互
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
            EditableDiceUIManager.Instance.SetActivity(true);//设置侧边物UI可交互
            strengthenCanvas.transform.DOMoveX(disappearTransformPosition.position.x, animTime);
            StartCoroutine(LateInactive(animTime, strengthenCanvas));
        }

        IEnumerator LateInactive(float time, GameObject objectX)
        {
            yield return new WaitForSeconds(time);
            objectX.SetActive(false);
            StoreAreaUIManager.Instance.SetButton(); //设置强化按钮可以交互
        }

        //private void Update()
        //{
        //    //if (Input.GetKeyDown(KeyCode.Space))
        //    //{
        //    //    StoreManager.Instance.OnEnterStore.Invoke();
        //    //}
        //}
    }
}
