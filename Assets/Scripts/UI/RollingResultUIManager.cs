using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace UI
{
    public enum Strategy
    {
        UseAll,
        ReRoll,
        End
    }
    public class RollingResultUIManager : MonoSingleton<RollingResultUIManager>
    {
        [SerializeField, Tooltip("投掷结果栏位")] private List<Column> columnList;
        [SerializeField, Tooltip("生成模板")] private GameObject template;
        [SerializeField, Tooltip("结果列表")] private List<GameObject> resultList;
        
        [Tooltip("使用动画时长")]public float useTime;
        [Tooltip("使用放大倍数")]public float scale;
        [Tooltip("距离使用者上方位置")] public float moveOffset;

        /// <summary>
        /// 生成投掷结果函数,请按投掷顺序有序生成
        /// </summary>
        /// <param name="index">是第几个投掷结果</param>
        /// <param name="data">骰面的数据</param>
        /// <param name="location">坐标位置，先页面位置再页内位置</param>
        /// <param name="ifFightEnd"></param>
        public void CreateResult(int index, SingleDiceUIData data, Vector2Int location,bool ifFightEnd)
        {
            var tmp = Instantiate(template, columnList[index].transform, true);
            tmp.transform.position = columnList[index].transform.position;//更改位置
            columnList[index].bagObject = tmp;
            var tmpResult = tmp.GetComponent<RollingResultDiceUI>();
            tmpResult.Init(index, location, data);//初始化
            if (ifFightEnd) { tmpResult.Disable();}
            tmp.SetActive(true);
            tmpResult.DoAppearAnim(useTime);//出现动画
            resultList.Add(tmp);
        }

        /// <summary>
        /// 摧毁所有结果
        /// </summary>
        public void RemoveAllResultUI(Strategy strategy)
        {
            if (resultList.Count == 0) { return;}

            foreach (var column in columnList)
            {
                column.bagObject = null;
            }
            
            var tmpList = new List<GameObject>();//备份队列
            while (resultList.Count != 0)
            {
                tmpList.Add(resultList[0]);
                resultList.Remove(resultList[0]);
            }
            resultList.Clear();//立刻清空队列避免报错

            switch (strategy)
            {
                case Strategy.End:
                    StartCoroutine(WaitForDestroyAll(ProcessAnimationManager.Instance.timeInterval,tmpList));//等待一段时间后销毁
                    return;
                case Strategy.UseAll:
                    StartCoroutine(WaitForDestroy(useTime, tmpList));
                    return;
                case Strategy.ReRoll:
                    StartCoroutine(DestroyAll(tmpList));
                    return;
            }
        }

        /// <summary>
        /// 摧毁单个结果
        /// </summary>
        /// <param name="index"></param>
        public void RemoveResultUI(int index)
        {
            if (index < 0 || index >= columnList.Count)
            {
                Debug.LogWarning("RollingResultUIManager:序列索引越界");
                return;
            }
            if (columnList[index].bagObject == null)
            {
                Debug.LogWarning("RollingResultUIManager:该投掷结果为空");
                return;
            }
            columnList[index].bagObject.GetComponent<RollingResultDiceUI>()?.OnUseDestroy();
            columnList[index].bagObject = null;
        }
        
        /// <summary>
        /// 使用所有骰面的函数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="tmp"></param>
        /// <returns></returns>
        IEnumerator WaitForDestroy(float time, List<GameObject> tmp)
        {
            while (tmp.Count != 0)
            {
                if (tmp[0] != null)
                {
                    tmp[0].GetComponent<RollingResultDiceUI>()?.OnUseDestroy();
                }
                yield return new WaitForSeconds(time);
                tmp.Remove(tmp[0]);
            }
        }

        /// <summary>
        /// 延时摧毁所有结果协程
        /// </summary>
        /// <param name="time">延迟时间</param>
        /// <returns></returns>
        IEnumerator WaitForDestroyAll(float time, List<GameObject> tmp)
        {
            while (tmp.Count != 0)
            {
                if (tmp[0] != null)
                {
                    yield return new WaitForSeconds(time);
                    Destroy(tmp[0].gameObject);
                }
                tmp.Remove(tmp[0]);
            }
        }
        
        /// <summary>
        /// 无延时摧毁所有结果协程
        /// </summary>
        /// <param name="time">延迟时间</param>
        /// <returns></returns>
        IEnumerator DestroyAll(List<GameObject> tmp)
        {
            while (tmp.Count != 0)
            {
                if (tmp[0] != null)
                {
                    tmp[0].GetComponent<RollingResultDiceUI>()?.OnReRollDestroy(); //重投函数
                }
                tmp.Remove(tmp[0]);
            }
            yield return new WaitForSeconds(useTime / 2);
        }

    }
}
