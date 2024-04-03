using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

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
        [SerializeField, Tooltip("投掷结果栏位")] private List<Transform> columns;
        [SerializeField, Tooltip("生成模板")] private GameObject template;
        [SerializeField, Tooltip("结果列表")] private List<GameObject> _resultList;
        
        [Tooltip("使用动画时长")]public float useTime;
        [Tooltip("使用放大倍数")]public float scale;
        [Tooltip("距离使用者上方位置")] public float moveOffset;

        /// <summary>
        /// 生成投掷结果函数,请按投掷顺序有序生成
        /// </summary>
        /// <param name="index">是第几个投掷结果</param>
        /// <param name="id">骰面的id</param>
        /// <param name="location">坐标位置，先页面位置再页内位置</param>
        /// <param name="ifFightEnd"></param>
        public void CreateResult(int index, string id, Vector2Int location,bool ifFightEnd)
        {
            var tmp = Instantiate(template, columns[index], true);
            tmp.transform.position = columns[index].position;//更改位置
            tmp.GetComponent<RollingResultDiceUI>().Init(index, location, id);//初始化
            if (ifFightEnd) { tmp.GetComponent<Button>().interactable = false;}
            tmp.SetActive(true);
            _resultList.Add(tmp);
        }

        /// <summary>
        /// 摧毁所有结果
        /// </summary>
        public void RemoveAllResultUI(Strategy strategy)
        {
            if (_resultList.Count == 0) { return;}

            var tmpList = new List<GameObject>();//备份队列
            while (_resultList.Count != 0)
            {
                tmpList.Add(_resultList[0]);
                _resultList.Remove(_resultList[0]);
            }
            _resultList.Clear();//立刻清空队列避免报错

            switch (strategy)
            {
                case Strategy.End:
                    StartCoroutine(WaitForDestroyAll(ProcessAnimationManager.Instance.timeInterval,tmpList));//等待一段时间后销毁
                    return;
                case Strategy.UseAll:
                    StartCoroutine(WaitForDestroy(useTime / 2, tmpList));
                    return;
                case Strategy.ReRoll:
                    StartCoroutine(DestroyAll(tmpList));
                    return;
            }
            
            
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
                    tmp[0].GetComponent<RollingResultDiceUI>().OnUseDestory();
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
                    yield return new WaitForSeconds(0);
                    Destroy(tmp[0].gameObject);
                }
                tmp.Remove(tmp[0]);
            }
        }

    }
}
