using System;
using UnityEngine;
using System.Collections.Generic;

namespace UI
{
    public class RollingResultManager : MonoSingleton<RollingResultManager>
    {
        [SerializeField, Tooltip("投掷结果栏位")] private List<Transform> columns;
        [SerializeField, Tooltip("生成模板")] private GameObject template;
        [SerializeField, Tooltip("生成模板")] private Transform parent;
        [SerializeField, Tooltip("结果列表")] private List<GameObject> _resultList;

        /// <summary>
        /// 生成投掷结果函数,请按投掷顺序有序生成
        /// </summary>
        /// <param name="index">是第几个投掷结果</param>
        /// <param name="id">骰面的id</param>
        /// <param name="location">坐标位置，先页面位置再页内位置</param>
        public void CreateResult(int index, string id, Vector2Int location)
        {
            var tmp = Instantiate(template, parent, true);
            tmp.transform.position = columns[index].position;//更改位置
            tmp.GetComponent<RollingResultDiceUI>().Init(index, location, id);//初始化
            tmp.SetActive(true);
            _resultList.Add(tmp);
        }

        /// <summary>
        /// 摧毁所有结果
        /// </summary>
        public void RemoveAllResultUI()
        {
            foreach (var result in _resultList)
            {
                Destroy(result);
            }
            _resultList.Clear();
        }
    }
}
