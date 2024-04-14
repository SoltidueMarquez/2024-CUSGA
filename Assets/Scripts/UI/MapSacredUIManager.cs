using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MapSacredUIManager : MonoSingleton<MapSacredUIManager>
    {
        [Tooltip("圣物晃动的角度")] public float shakeAngle;
        [Tooltip("圣物变大的倍数")] public float previewSize;
        [Tooltip("圣物栏列表")] public List<Column> sacredObjectColumns;
        [Tooltip("栏目判定")] public float offsetS;
        [SerializeField, Tooltip("生成模板")] private GameObject template;
        [SerializeField, Tooltip("父物体")] public Transform parent;
        [SerializeField, Tooltip("闪光特效")] public GameObject flickEffect;
        [Tooltip("浮动距离")] public float flowDis;
        
        /// <summary>
        /// 初始化圣物栏
        /// </summary>
        /// <param name="halidomObject"></param>
        /// <param name="removeList"></param>
        public void Init(List<HalidomObject> halidomObject, List<Action<HalidomObject>> removeList)
        {
            RemoveAllSacredObject();
            for (int i = 0; i < halidomObject.Count; i++)
            {
                CreateSacredUIObject(i, halidomObject[i].id, removeList[i], halidomObject[i]);
                //CreateSacredUIObject(i, "1_02", null, null);
            }
        }
        
        /// <summary>
        /// 生成圣物函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        /// <param name="id"></param>
        /// <param name="remove"></param>
        private void CreateSacredUIObject(int index, string id, Action<HalidomObject> remove,HalidomObject halidomObject)
        {
            if (index >= sacredObjectColumns.Count)
            {
                Debug.LogWarning("错误,圣物栏位溢出");
                return;
            }
            if (sacredObjectColumns[index].bagObject != null)
            {
                Debug.LogWarning("错误，所在栏位已经有圣物存在");
                return;
            }
            var tmp = Instantiate(template, parent, true);
            tmp.transform.position = sacredObjectColumns[index].transform.position;//更改位置
            tmp.GetComponent<MapSacredUIObject>().Init(sacredObjectColumns, offsetS, id, remove, halidomObject);//初始化
            //tmp.GetComponent<MapSacredUIObject>().Init(sacredObjectColumns, offsetS, id);//初始化
            tmp.SetActive(true);
        }
        
        /// <summary>
        /// 按照栏位的真实位置移除圣物函数
        /// </summary>
        /// <param name="index">所在栏位序列号</param>
        private void RemoveSacredObject(int index)
        {
            if (sacredObjectColumns[index].bagObject == null)
            {
                Debug.Log("错误，所在栏位没有圣物存在");
                return;
            }
            var tmp = sacredObjectColumns[index].bagObject.GetComponent<MapSacredUIObject>();
            tmp.DestroyUI(0);
        }
        private void RemoveAllSacredObject()
        {
            for (int i = 0; i < sacredObjectColumns.Count; i++)
            {
                if (sacredObjectColumns[i].bagObject != null)
                {
                    RemoveSacredObject(i);
                }
            }
        }
        
        /// <summary>
        /// 圣物的闪烁函数
        /// </summary>
        /// <param name="id">圣物标识</param>
        public void DoFlick(string id)
        {
            MapSacredUIObject sacredUI;
            foreach (var sacred in sacredObjectColumns)
            {
                if (sacred.bagObject == null)
                {
                    continue;
                }
                sacredUI = sacred.bagObject.GetComponent<MapSacredUIObject>();
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
                    continue;
                }
                var sacredUI = sacred.bagObject.GetComponent<MapSacredUIObject>();
                scaredObjectIDList.Add(sacredUI != null ? sacredUI.id : "");
            }
            return scaredObjectIDList;
        }
        
        /*#region 测试
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                
            }
        }
        #endregion*/
    }
}
