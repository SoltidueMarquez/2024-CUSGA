using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Settlement_Scene
{
    public enum SettlementDataType
    {
        Layer,
        NormalEnemyNum,
        BossNum,
        Money,
        SacredObjectNum,
        DiceNum,
        IfWin
    }
    public class SettlementManager : MonoSingleton<SettlementManager>
    {
        [Tooltip("进入结算界面事件")]public UnityEvent onEnterSettlement;
        [Tooltip("进入结算界面事件")]public UnityEvent onExitSettlement;

        [SerializeField, Tooltip("数据列表")] private List<SettlementData> dataList;

        private void Start()
        {
            onEnterSettlement.AddListener(Init);
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
        private Vector2 ReadInfo(SettlementDataType dataType)
        {
            return new Vector2(Random.Range(0, 10), Random.Range(0, 10));
        }

        #region 测试
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                onEnterSettlement.Invoke();
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log($"SettlementManager:Total Score is <color=green>{CalculateTotalScore()}</color>");
                onExitSettlement.Invoke();
            }
        }
        #endregion
    }
}
