using System;
using System.Collections.Generic;
using UI.Settlement;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Settlement_Scene
{
    public enum SettlementDataType
    {
        层数,
        敌人,
        Boss,
        金钱,
        圣物,
        骰面,
        是否通关
    }
    public class SettlementManager : MonoSingleton<SettlementManager>
    {
        [Tooltip("进入结算界面事件")]public UnityEvent onEnterSettlement;
        [Tooltip("离开结算界面事件")]public UnityEvent onExitSettlement;

        [SerializeField, Tooltip("数据列表")] private List<SettlementData> dataList;

        private void Start()
        {
            onEnterSettlement.AddListener(Init);
            onEnterSettlement.AddListener(()=> { SettlementUIManager.Instance.ShowScore(dataList); });
        }

        /// <summary>
        /// 初始化数据列表的信息
        /// </summary>
        private void Init()
        {
            foreach (var data in dataList)
            {
                data.Init(ReadInfo);
            }
        }

        /// <summary>
        /// 获取总计分数
        /// </summary>
        /// <returns></returns>
        public float CalculateTotalScore()
        {
            float totalScore = 0;
            foreach (var data in dataList)
            {
                totalScore += data.GetResultScore();
            }
            return totalScore;
        }

        //TODO:调用外部存档的接口获取信息
        private float ReadInfo(SettlementDataType dataType)
        {
            return Random.Range(0, 10);
        }

        #region 测试
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                onEnterSettlement.Invoke();
            }
        }
        #endregion
    }
}
