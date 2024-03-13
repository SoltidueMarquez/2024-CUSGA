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
    /// 基础伤害
    /// </summary>
    public int Damage;
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
}


