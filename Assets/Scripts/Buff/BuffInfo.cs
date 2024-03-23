using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffInfo
{
    /// <summary>
    /// buff数据的模板
    /// </summary>
    public BuffData buffData;
    /// <summary>
    /// buff是否是永久的
    /// </summary>
    public bool isPermanent;
    /// <summary>
    /// buff的创建者
    /// </summary>
    public GameObject creator = null;
    /// <summary>
    /// buff的目标
    /// </summary>
    public GameObject target = null;
    /// <summary>
    /// 回合计数器
    /// </summary>
    public int roundCount;
    /// <summary>
    /// 执行次数
    /// </summary>
    public int ticked = 0;
    /// <summary>
    /// 当前buff的层数
    /// </summary>
    public int curStack;

    public Dictionary<string, object> buffParam = new Dictionary<string, object>();

    public BuffInfo(
        BuffData buffData, GameObject creator, GameObject target, int stack = 1, bool isPermanent = false,
        Dictionary<string, object> buffParam = null
        )
    {
        this.buffData = buffData;
        this.creator = creator;
        this.target = target;
        this.curStack = stack;
        this.isPermanent = isPermanent;
        this.roundCount = buffData.duringCount;
        if (buffParam != null)
        {
            foreach (var item in buffParam)
            {
                this.buffParam.Add(item.Key, item.Value);
            }
        }
    }

    public BuffInfo(
        BuffData buffData,int stack = 1, bool isPermanent = false,
        Dictionary<string, object> buffParam = null
        )
    {
        this.buffData = buffData;
        this.curStack = stack;
        this.isPermanent = isPermanent;
        this.roundCount = buffData.duringCount;
        if (buffParam != null)
        {
            foreach (var item in buffParam)
            {
                this.buffParam.Add(item.Key, item.Value);
            }
        }
    }
}

public enum BuffRemoveStackUpdateEnum
{
    Clear,
    Reduce
}

public enum  BuffUpdateEnum
{
    Add,
    Replace,
    Keep
}

