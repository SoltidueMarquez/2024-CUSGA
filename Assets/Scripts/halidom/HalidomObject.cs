using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RareType
{
    Common,
    Rare,
    Legendary
}
public class HalidomObject
{
    /// <summary>
    /// 圣物唯一id标识
    /// </summary>
    public string id;
    /// <summary>
    /// 圣物名称
    /// </summary>
    public string halidomName;
    /// <summary>
    /// 圣物描述
    /// </summary>
    public string description;
    /// <summary>
    /// 圣物身上存的buff信息
    /// </summary>
    public List<BuffInfo> buffInfos;
    /// <summary>
    /// 圣物在格子中的序号
    /// </summary>
    public int halidomIndex;
    /// <summary>
    /// 圣物的稀有度
    /// </summary>
    public RareType rareType;
    /// <summary>
    /// 圣物的购入价格
    /// </summary>
    public int value;
    /// <summary>
    /// 圣物的出售价格
    /// </summary>
    public int saleValue => Mathf.FloorToInt(value / 4);

    public HalidomObject(RareType rareType,string id, string halidomName, string description,  int value,List<BuffInfo> buffinfo )
    {
        this.rareType = rareType;
        this.id = id;
        this.halidomName = halidomName;
        this.description = description;
        this.value = value;
        this.buffInfos = buffinfo;
    }

    
}




