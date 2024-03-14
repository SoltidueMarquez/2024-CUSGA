using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单一的骰子面的数据，其中包含了效果的数据
/// </summary>
public class SingleDiceModel
{
    /// <summary>
    /// 骰子单面的唯一id
    /// </summary>
    public string id;
    /// <summary>
    /// 骰子的使用条件
    /// </summary>
    public ChaResource condition;
    /// <summary>
    /// 骰子的消耗
    /// </summary>
    public ChaResource cost;
    /// <summary>
    /// 基础伤害,这个伤害是不包含等级的伤害
    /// </summary>
    public Damage damage;
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
    /// <summary>
    /// 使用骰面的特效
    /// </summary>
    public BuffInfo[] buffInfos;
    /// <summary>
    /// 骰子的特效
    /// </summary>
    public VisualEffect visualEffect;

    public SingleDiceModel(string id, ChaResource condition, ChaResource cost, Damage damage, int level, BuffInfo[] buffInfos, VisualEffect visualEffect)
    {
        this.id = id;
        this.condition = condition;
        this.cost = cost;
        this.damage = damage;
        this.level = level;
        this.buffInfos = buffInfos;
        this.visualEffect = visualEffect;
    }
}


