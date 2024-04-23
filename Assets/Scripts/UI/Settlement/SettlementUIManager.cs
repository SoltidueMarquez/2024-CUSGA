using System;
using System.Collections.Generic;
using DG.Tweening;
using Settlement_Scene;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Settlement
{
    public class SettlementUIManager : MonoSingleton<SettlementUIManager>
    {
        [SerializeField] private List<GameObject> settlementTextList;
        [SerializeField] private Text totalScoreText;
        [SerializeField] private GameObject template;
        [SerializeField] private Transform parent;
        [SerializeField] private float animTime;
        [SerializeField] private Button exitButton;
        [SerializeField] private GameObject uiCanvas;

        private void Start()
        {
            settlementTextList.Clear();
            SettlementManager.Instance.onEnterSettlement.AddListener(EnterUI);
            SettlementManager.Instance.onExitSettlement.AddListener(ExitUI);
            exitButton.onClick.AddListener(() =>
            {
                SettlementManager.Instance.onExitSettlement.Invoke();
            });
        }

        /// <summary>
        /// 创建数据分数并展示
        /// </summary>
        /// <param name="dataList"></param>
        public void ShowScore(List<SettlementData> dataList)
        {
            foreach (var data in dataList)
            {
                var tmp = Instantiate(template, parent, true);
                tmp.SetActive(true);
                var content = $"{data.dataType}:{data.data.x}..........................{data.GetResultScore()}";
                var tmpText = tmp.GetComponent<Text>();
                tmpText.DOText(content, animTime);
                settlementTextList.Add(tmp);
            }
            totalScoreText.DOText($"共计拿到了{SettlementManager.Instance.CalculateTotalScore()}分", animTime);
        }

        /// <summary>
        /// 离开界面
        /// </summary>
        private void ExitUI()
        {
            uiCanvas.SetActive(false);
            foreach (var txt in settlementTextList)
            {
                Destroy(txt);
            }
            settlementTextList.Clear();
        }

        private void EnterUI()
        {
            uiCanvas.SetActive(true);
        }
    }
}
