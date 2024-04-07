using System;
using UnityEngine;
using System.Collections.Generic;

namespace UI
{
    public class SacredObjectUIManager : MonoSingleton<SacredObjectUIManager>
    {
        [Header("圣物相关")] 
        [Tooltip("圣物晃动的角度")] public float shakeAngleS;
        [Tooltip("圣物变大的倍数")] public float previewSizeS;
        [Tooltip("圣物栏列表")] public List<Column> sacredObjectColumns;
        [Tooltip("栏目判定")] public float offsetS;
        [SerializeField, Tooltip("生成模板")] private GameObject template;
        [SerializeField, Tooltip("父物体")] private Transform parent;
        [SerializeField, Tooltip("闪光特效")] public GameObject flickEffect;

        /// <summary>
        /// 生成圣物函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        /// <param name="id"></param>
        /// <param name="remove"></param>
        public void CreateSacredUIObject(int index, string id, Action<HalidomObject> remove,HalidomObject halidomObject)
        {
            if (sacredObjectColumns[index].bagObject != null)
            {
                Debug.LogWarning("错误，所在栏位已经有圣物存在");
                return;
            }
            var tmp = Instantiate(template, parent, true);
            tmp.transform.position = sacredObjectColumns[index].transform.position;//更改位置
            tmp.GetComponent<SacredObjectsUIEffects>().Init(sacredObjectColumns, offsetS, id, remove, halidomObject);//初始化
            tmp.SetActive(true);
        }

        /// <summary>
        /// 按照栏位的真实位置移除圣物函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        public void RemoveSacredObject(int index)
        {
            if (sacredObjectColumns[index].bagObject == null)
            {
                Debug.Log("错误，所在栏位没有圣物存在");
                return;
            }
            var tmp = sacredObjectColumns[index].bagObject;
            Destroy(tmp);
            sacredObjectColumns[index].bagObject = null;
        }

        /// <summary>
        /// 圣物的闪烁函数
        /// </summary>
        /// <param name="id">圣物标识</param>
        public void DoFlick(string id)
        {
            SacredObjectsUIEffects sacredUI;
            foreach (var sacred in sacredObjectColumns)
            {
                if (sacred.bagObject == null)
                {
                    continue;
                }
                sacredUI = sacred.bagObject.GetComponent<SacredObjectsUIEffects>();
                if (sacredUI != null && sacredUI.id == id)
                {
                    sacredUI.DoFlick();
                }
            }
        }

        /// <summary>
        /// 获取圣物id列表，对应栏位为空时id为""
        /// </summary>
        public List<string> GetScaredObjectIDList()
        {
            List<string> scaredObjectIDList = new List<string>();
            foreach (var sacred in sacredObjectColumns)
            {
                if (sacred.bagObject == null)//如果没有就加入""
                {
                    scaredObjectIDList.Add("");
                }
                var sacredUI = sacred.bagObject.GetComponent<SacredObjectsUIEffects>();
                if (sacredUI != null)
                {
                    scaredObjectIDList.Add(sacredUI.id);
                }
                else
                {
                    scaredObjectIDList.Add("");
                }
            }
            return scaredObjectIDList;
        }
    }
}
