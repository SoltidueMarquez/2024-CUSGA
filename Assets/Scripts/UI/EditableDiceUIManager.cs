using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class EditableDiceUIManager : MonoSingleton<EditableDiceUIManager>
    {
        [Header("动画设置")]
        [Tooltip("骰面晃动的角度")] public float shakeAngle;
        [Tooltip("骰面变大的倍数")] public float previewSize;
        [Tooltip("浮动距离")] public float flowDis;
        
        [Header("栏位设置")]
        [Tooltip("栏目判定")] public float offset;
        [Tooltip("战斗栏列表")] public List<Column> fightColumns;
        [Tooltip("背包栏列表")] public List<Column> bagColumns;
        public List<Column> allColumns;
        
        [Header("模板设置")]
        [Tooltip("所有骰子的父物体")] public Transform parent;
        [SerializeField, Tooltip("生成模板")] private GameObject template;

        private void Start()
        {
            foreach (var column in fightColumns)
            {
                column.state = EditState.FightDice;
                allColumns.Add(column);
            }
            foreach (var column in bagColumns)
            {
                column.state = EditState.BagDice;
                allColumns.Add(column);
            }
        }

        #region 背包骰面
        /// <summary>
        /// 生成单个背包骰面函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        /// <param name="remove"></param>
        /// <param name="singleDiceObj"></param>
        private void CreateBagUIDice(int index, SingleDiceObj singleDiceObj, Action<SingleDiceObj> remove)
        {
            if (bagColumns[index].bagObject != null)
            {
                Debug.LogWarning("错误，所在栏位已经有骰面存在");
                return;
            }
            var tmp = Instantiate(template, parent, true);
            tmp.transform.position = bagColumns[index].transform.position;//更改位置
            //tmp.GetComponent<EditableDiceUIObject>().Init(bagColumns, offset, singleDiceObj, remove);//初始化
            tmp.GetComponent<EditableDiceUIObject>().Init(bagColumns, offset);
            tmp.SetActive(true);
        }

        public void CreateBagUIDice(List<SingleDiceObj> diceList, List<Action<SingleDiceObj>> removeList)
        {
            for (int i = 0; i < diceList.Count; i++)
            {
                CreateBagUIDice(i, diceList[i], removeList[i]);
            }
        }
        
        /// <summary>
        /// 移除背包骰面函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        private void RemoveBagDice(int index)
        {
            if (bagColumns[index].bagObject == null)
            {
                Debug.LogWarning("错误，所在栏位没有骰面存在");
                return;
            }
            var tmp = bagColumns[index].bagObject.GetComponent<EditableDiceUIObject>();
            tmp.DestroyUI(0);
        }

        public void RemoveAllBagDice()
        {
            for (int i = 0; i < bagColumns.Count; i++)
            {
                if (bagColumns[i].bagObject != null)
                {
                    RemoveBagDice(i);
                }
            }
        }
        #endregion

        #region 测试
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                for (int i = 0; i < 3; i++)
                {
                    CreateBagUIDice(i, null, null);
                }
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                for (int i = 0; i < 6; i++)
                {
                    RemoveAllBagDice();
                }
            }
        }
        #endregion
        
    }
}
