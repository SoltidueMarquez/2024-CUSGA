using System;
using UnityEngine;
using System.Collections.Generic;

namespace UI
{
    public class BagDiceUIManager : MonoSingleton<BagDiceUIManager>
    {
        [Header("背包骰面相关")]
        [Tooltip("骰面晃动的角度")] public float shakeAngleB;
        [Tooltip("骰面变大的倍数")] public float previewSizeB;
        [Tooltip("背包栏列表")] public List<Column> bagColumns;
        [Tooltip("栏目判定")] public float offsetB;
        [SerializeField, Tooltip("生成模板")] private GameObject template;
        [SerializeField, Tooltip("父物体")] private Transform parent;

        /// <summary>
        /// 生成圣物函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        /// <param name="data"></param>
        /// <param name="remove"></param>
        public void CreateBagUIDice(int index, SingleDiceUIData data, Action<int> remove)
        {
            if (bagColumns[index].bagObject != null)
            {
                Debug.Log("错误，所在栏位已经有骰面存在");
                return;
            }
            var tmp = Instantiate(template, parent, true);
            tmp.transform.position = bagColumns[index].transform.position;//更改位置
            tmp.GetComponent<BagDiceUIEffects>().Init(bagColumns, offsetB, data, remove, index);//初始化
            tmp.SetActive(true);
        }
        /// <summary>
        /// 移除圣物函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        public void RemoveBagDice(int index)
        {
            if (bagColumns[index].bagObject == null)
            {
                Debug.Log("错误，所在栏位没有骰面存在");
                return;
            }
            var tmp = bagColumns[index].bagObject;
            Destroy(tmp);
            bagColumns[index].bagObject = null;
        }
    }
}
