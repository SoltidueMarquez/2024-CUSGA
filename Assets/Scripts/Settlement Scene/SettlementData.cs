using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settlement_Scene
{
    [Serializable]
    public class SettlementData
    {
        /// <summary>
        /// _data.x表示数据，_data.y表示乘算因子
        /// </summary>
        [SerializeField] public Vector2 data;
        [SerializeField] public SettlementDataType dataType;

        public void Init(Func<SettlementDataType, float> getData)
        {
            data.x = getData.Invoke(dataType);
        }
        
        public float GetResultScore()
        {
            float res = 0;
            res = data.x * data.y;//数据乘以乘算因子就是当前数据项的最终得分
            return res;
        }
    }
}
