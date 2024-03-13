using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffData
{
    /// <summary>
    /// buff的唯一id
    /// </summary>
    public string id;
    /// <summary>
    /// buff的名字
    /// </summary>
    public string buffName;
    /// <summary>
    /// 存储buff的icon图标的路径，resoueces读取
    /// </summary>
    public string buffIcon;
    /// <summary>
    /// 用于方便检索的tag，例如：火焰，冰冻，中毒等
    /// </summary>
    public string[] tags;
    /// <summary>
    /// buff的最高层数
    /// </summary>
    public int maxStack;

    //buff的时间信息
    public int duringCount;
    /// <summary>
    /// 是否是永久的buff
    /// </summary>
    public bool isPermanent;
    /// <summary>
    /// buff会给角色添加的属性，暂定两种，一种加算，一种乘算
    /// </summary>
    public ChaProperty[] propMod;

}
public delegate void BuffOnCreate(BuffInfo buff);
public delegate void BuffOnRemove(BuffInfo buff);
public delegate void BuffOnRound(BuffInfo buff);
public delegate void BuffOnHit(BuffInfo buff);

