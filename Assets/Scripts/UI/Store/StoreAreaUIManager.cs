using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Store
{
    public class StoreAreaUIManager : MonoSingleton<StoreAreaUIManager>
    {
        [Header("通用")]
        [SerializeField, Tooltip("动画时长")] private float animTime;
        [Tooltip("预览大小")] public float previewSize;
        [Tooltip("晃动角度")] public float shakeAngle;

        [Header("出售骰面")]
        [SerializeField, Tooltip("骰子栏位列表")] public List<Column> diceColumns;
        [SerializeField, Tooltip("骰子模板")] private GameObject diceTemplate;
        
        [Header("出售圣物")]
        [SerializeField, Tooltip("圣物栏位列表")] public List<Column> sacredObjectColumns;
        [SerializeField, Tooltip("圣物模板")] private GameObject sacredObjectTemplate;

        #region 出售骰面相关
        /// <summary>
        /// 创建骰面函数,其本质仍然是一个战斗骰面页的骰子(子类)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index">栏位索引</param>
        /// <param name="onChoose">选择骰面后触发的逻辑函数</param>
        /// <param name="singleDiceObj">骰面物体</param>
        public void CreateDiceUI(SingleDiceUIData data, int index, Action<SingleDiceObj> onChoose,SingleDiceObj singleDiceObj)
        {
            if (index > diceColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (diceColumns[index].bagObject != null) 
            {
                Debug.LogWarning("栏位已经有骰面");
                return;
            }
            var parent = diceColumns[index].transform;
            var tmp = Instantiate(diceTemplate, parent, true);
            diceColumns[index].bagObject = tmp;
            tmp.transform.position = parent.position;//更改位置
            var tmpDice = tmp.GetComponent<StoreDiceUIObject>();
            tmpDice.Init(data, animTime, 2, onChoose, singleDiceObj);//初始化
            tmp.SetActive(true);
            tmpDice.DoAppearAnim(animTime); //出现动画
        }
        
        /// <summary>
        /// 移除骰面函数
        /// </summary>
        /// <param name="index">栏位索引</param>
        public void RemoveDiceUI(int index)
        {
            if (index > diceColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (diceColumns[index].bagObject == null) 
            {
                Debug.LogWarning("骰面不存在");
                return;
            }
            var item = diceColumns[index].bagObject.GetComponent<StoreDiceUIObject>();
            diceColumns[index].bagObject = null;
            if (item != null)
            {
                item.DoDestroyAnim(animTime);
            }
        }
        
        /// <summary>
        /// 禁用全部骰面函数
        /// </summary>
        public void DisableAllDices()
        {
            foreach (var col in diceColumns)
            {
                if (col.bagObject != null)
                {
                    var item = col.bagObject.GetComponent<StoreDiceUIObject>();
                    item.Disable();
                }
            }
        }
        
        /// <summary>
        /// 启用全部骰面函数
        /// </summary>
        public void EnableAllDices()
        {
            foreach (var col in diceColumns)
            {
                if (col.bagObject != null)
                {
                    var item = col.bagObject.GetComponent<StoreDiceUIObject>();
                    item.Enable();
                }
            }
        }
        #endregion

        #region 出售圣物相关
        /// <summary>
        /// 创建圣物函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <param name="onChoose"></param>
        /// <param name="halidomObject"></param>
        public void CreateSacredObject(string id, int index, Action<HalidomObject> onChoose,HalidomObject halidomObject)
        {
            if (index > sacredObjectColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (sacredObjectColumns[index].bagObject != null) 
            {
                Debug.LogWarning("栏位已经有圣物");
                return;
            }
            var parent = sacredObjectColumns[index].transform;
            var tmp = Instantiate(sacredObjectTemplate, parent, true);
            sacredObjectColumns[index].bagObject = tmp;
            tmp.transform.position = parent.position;//更改位置
            var tmpSacredObject = tmp.GetComponent<StoreSacredUIObject>();
            tmpSacredObject.Init(id, animTime, 2, onChoose, halidomObject);//初始化
            tmp.SetActive(true);
            tmpSacredObject.DoAppearAnim(animTime); //出现动画
        }
        
        /// <summary>
        /// 移除圣物函数
        /// </summary>
        /// <param name="index">栏位索引</param>
        public void RemoveSacredObject(int index)
        {
            if (index > sacredObjectColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (sacredObjectColumns[index].bagObject == null) 
            {
                Debug.LogWarning("圣物不存在");
                return;
            }
            var item = sacredObjectColumns[index].bagObject.GetComponent<StoreSacredUIObject>();
            sacredObjectColumns[index].bagObject = null;
            if (item != null)
            {
                item.DoDestroyAnim(animTime);
            }
        }
        
        /// <summary>
        /// 禁用全部圣物函数
        /// </summary>
        public void DisableAllSacredObject()
        {
            foreach (var col in sacredObjectColumns)
            {
                if (col.bagObject != null)
                {
                    var item = col.bagObject.GetComponent<StoreSacredUIObject>();
                    item.Disable();
                }
            }
        }

        /// <summary>
        /// 启用全部圣物函数
        /// </summary>
        public void EnableAllSacredObject()
        {
            foreach (var col in sacredObjectColumns)
            {
                if (col.bagObject != null)
                {
                    var item = col.bagObject.GetComponent<StoreSacredUIObject>();
                    item.Enable();
                }
            }
        }
        #endregion
        
    }
}
