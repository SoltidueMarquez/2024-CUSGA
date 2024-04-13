using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class EditableDiceUIManager : MonoSingleton<EditableDiceUIManager>
    {
        [Header("背包骰面相关")]
        [Tooltip("骰面晃动的角度")] public float shakeAngle;
        [Tooltip("骰面变大的倍数")] public float previewSize;
        [Tooltip("栏目判定")] public float offsetB;
        
        [Tooltip("战斗栏列表")] public List<Column> fightColumns;
        [Tooltip("背包栏列表")] public List<Column> bagColumns;
        
        [SerializeField, Tooltip("生成模板")] private GameObject template;


        /// <summary>
        /// 生成单个背包骰面函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        /// <param name="remove"></param>
        /// <param name="singleDiceObj"></param>
        public void CreateBagUIDice(int index, Action<SingleDiceObj> remove,SingleDiceObj singleDiceObj)
        {
            if (bagColumns[index].bagObject != null)
            {
                Debug.LogWarning("错误，所在栏位已经有骰面存在");
                return;
            }
            var tmp = Instantiate(template, bagColumns[index].transform, true);
            tmp.transform.position = bagColumns[index].transform.position;//更改位置
            tmp.GetComponent<EditableDiceUIObject>().Init(bagColumns, offsetB, singleDiceObj, remove);//初始化
            tmp.SetActive(true);
        }
        
        /// <summary>
        /// 移除背包骰面函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        public void RemoveBagDice(int index)
        {
            if (bagColumns[index].bagObject == null)
            {
                Debug.LogWarning("错误，所在栏位没有骰面存在");
                return;
            }
            var tmp = bagColumns[index].bagObject.GetComponent<EditableDiceUIObject>();
            tmp.DestroyUI();
        }
    }
}
