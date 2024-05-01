using System;
using System.Collections;
using System.Collections.Generic;
using Audio_Manager;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.Store
{
    public class RollUIManager : MonoSingleton<RollUIManager>
    {
        [SerializeField, Tooltip("旋转物体列表")] private List<RotateObject> rotateObjects;
        [SerializeField, Tooltip("出现结果列表")] private List<Transform> results;
        [SerializeField, Tooltip("出现时间间隔")] public float timeInterval;
        [SerializeField, Tooltip("出现位置(Y)")] private Transform enterPosition;
        [SerializeField, Tooltip("出现位置(Y)")] private Transform exitPosition;
        [SerializeField, Tooltip("投掷结果栏位")] private List<Column> columns;
        [SerializeField, Tooltip("生成模板")] private GameObject template;
        [SerializeField, Tooltip("结果列表")] private List<GameObject> resultList;
        [Tooltip("使用动画时长")]public float animTime;
        
        #region 轮盘按钮与栏位动画
        /// <summary>
        /// 轮盘与按钮旋转进入
        /// </summary>
        private void RotateIn(PlayerTurnState state)
        {
            float offset = 0f;
            switch (state)
            {
                case PlayerTurnState.PlayerTurnStart:
                    offset = 0;
                    break;
                case PlayerTurnState.PlayerTurnEnd:
                    offset = 180;
                    break;
                default:
                    break;
            }
            foreach (var rotateObject in rotateObjects)
            {
                var v3 = new Vector3(0, 0, rotateObject.rotateAngle-offset);
                rotateObject.transform.DOLocalRotate(v3, rotateObject.rotateTime, RotateMode.FastBeyond360);
            }
        }

        /// <summary>
        /// 结果出现
        /// </summary>
        /// <param name="state"></param>
        private void ResultAppear(PlayerTurnState state)
        {
            float position = 0f;
            switch (state)
            {
                case PlayerTurnState.PlayerTurnStart:
                    position = enterPosition.position.y;
                    break;
                case PlayerTurnState.PlayerTurnEnd:
                    position = exitPosition.position.y;
                    break;
                default:
                    position = enterPosition.position.y;
                    break;
            }
            float time = 0;
            foreach (var result in results)
            {
                time += timeInterval;
                result.DOMoveY(position, time);
            }
        }
        public void StoreAppear()
        {
            RotateIn(PlayerTurnState.PlayerTurnStart);
            ResultAppear(PlayerTurnState.PlayerTurnStart);
        }
        public void StoreDisAppear()
        {
            RotateIn(PlayerTurnState.PlayerTurnEnd);
            ResultAppear(PlayerTurnState.PlayerTurnEnd);
        }
        #endregion

        #region 投掷结果骰面相关显示
        /// <summary>
        /// 生成投掷结果函数,请按投掷顺序有序生成
        /// </summary>
        /// <param name="index">是第几个投掷结果</param>
        /// <param name="data">骰面的数据</param>
        /// <param name="location">坐标位置，先页面位置再页内位置</param>
        public void CreateResult(int index, SingleDiceUIData data, Vector2Int location)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayRandomSound("rollDice");
            }
            if (index >= columns.Count)
            {
                StoreManager.Instance.m_Debug("<color=#ff8400>错误，超出结果栏上限</color>");
                return;
            }
            if (columns[index].bagObject != null)
            {
                StoreManager.Instance.m_Debug("<color=#ff8400>错误，该结果栏已经存在骰面</color>");
                return;
            }
            var tmp = Instantiate(template, columns[index].transform, true);
            tmp.transform.position = columns[index].transform.position;//更改位置
            columns[index].bagObject = tmp;
            var tmpResult = tmp.GetComponent<StoreRollingResultUIObject>();//获取组件
            tmpResult.Init(location, data);//初始化
            tmp.SetActive(true);
            tmpResult.DoAppearAnim(animTime);//出现动画
            resultList.Add(tmp);
        }

        /// <summary>
        /// 摧毁所有结果
        /// </summary>
        public void RemoveAllResultUI()
        {
            if (resultList.Count == 0)
            {
                return;
            }
            var tmpList = new List<GameObject>(); //备份队列
            while (resultList.Count != 0)
            {
                tmpList.Add(resultList[0]);
                resultList.Remove(resultList[0]);
            }
            resultList.Clear(); //立刻清空队列避免报错
            foreach (var column in columns)
            {
                column.bagObject = null;
            }
            while (tmpList.Count != 0)
            {
                if (tmpList[0] != null)
                {
                    tmpList[0].GetComponent<StoreRollingResultUIObject>()?.OnReRollDestroy(animTime / 2); //重投函数
                }
                tmpList.Remove(tmpList[0]);
            }
        }
        #endregion
    }
}
