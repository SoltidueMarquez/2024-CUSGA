using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 角色的基础属性，最大生命值，金钱，重投次数，基础护盾,也是buff系统用来修改的依据
/// </summary>
[System.Serializable]
public class ChaProperty
{
    /// <summary>
    /// 最大生命值，在buff中即buff对生命值的修改
    /// </summary>
    [Tooltip("最大生命值")] public int health;
    /// <summary>
    /// 玩家的金钱，在buff中即buff对金钱的修改
    /// </summary>
    [Tooltip("玩家的金钱")] public int money;
    /// <summary>
    /// 最大重投次数，在buff中即buff对重投次数的修改
    /// </summary>
    [Tooltip("最大重投次数")] public int maxRollTimes;
    /// <summary>
    /// 玩家的护盾，在buff中即buff对护盾的修改
    /// </summary>
    [Tooltip("玩家的护盾")] public int shield;
    /// <summary>
    /// 玩家当前的最大使用点数
    /// </summary>
    [Tooltip("玩家的最大使用点数")] public int maxCost;

    //TODO:运算符重载
    /// <summary>
    /// 这边是最基础的构造函数，用于初始化
    /// </summary>
    /// <param name="health"></param>
    /// <param name="money"></param>
    /// <param name="maxRollTimes"></param>
    /// <param name="shield"></param>
    public ChaProperty(int health = 0, int money = 0, int maxRollTimes = 0, int shield = 0)
    {
        this.health = health;
        this.money = money;
        this.maxRollTimes = maxRollTimes;
        this.shield = shield;
    }
    public ChaProperty(int health = 0, int money = 0, int maxRollTimes = 0, int shield = 0, int maxCost = 0)
    {
        this.health = health;
        this.money = money;
        this.maxRollTimes = maxRollTimes;
        this.shield = shield;
        this.maxCost = maxCost;
    }
    public ChaProperty(ChaProperty chaProperty)
    {
        this.health = chaProperty.health;
        this.money = chaProperty.money;
        this.maxRollTimes = chaProperty.maxRollTimes;
        this.shield = chaProperty.shield;
        this.maxCost = chaProperty.maxCost;
    }
    public void Zero()
    {
        health = 0;
        money = 0;
        maxRollTimes = 0;
        shield = 0;
        maxCost = 0;
    }

    public static ChaProperty zero = new ChaProperty(0, 0, 0, 0, 0);


    public static ChaProperty operator +(ChaProperty a, ChaProperty b)
    {
        return new ChaProperty(a.health + b.health, a.money + b.money, a.maxRollTimes + b.maxRollTimes, a.shield + b.shield, a.maxCost + b.maxCost);
    }
    public static ChaProperty operator -(ChaProperty a, ChaProperty b)
    {
        return new ChaProperty(a.health - b.health, a.money - b.money, a.maxRollTimes - b.maxRollTimes, a.shield - b.shield, a.maxCost - b.maxCost);
    }
    public static ChaProperty operator *(ChaProperty chaProperty, float times)
    {
        return new ChaProperty(
            Mathf.RoundToInt(chaProperty.health * times),
            Mathf.RoundToInt(chaProperty.money * times),
            Mathf.RoundToInt(chaProperty.maxRollTimes * times),
            Mathf.RoundToInt(chaProperty.shield * times),
            Mathf.RoundToInt(chaProperty.maxCost * times)
            );
    }
    public static ChaProperty operator *(ChaProperty chaProperty, ChaProperty chaProperty1)
    {
        return new ChaProperty(
            Mathf.RoundToInt(chaProperty.health * (1.000f + chaProperty1.health)),
            Mathf.RoundToInt(chaProperty.money * (1.000f + Mathf.Max(chaProperty1.money, -0.9999f))),
            Mathf.RoundToInt(chaProperty.maxRollTimes * (1.000f + Mathf.Max(chaProperty1.maxRollTimes, -0.9999f))),
            Mathf.RoundToInt(chaProperty.shield * (1.000f + Mathf.Max(chaProperty1.shield, -0.9999f))),
            Mathf.RoundToInt(chaProperty.maxCost * (1.000f + Mathf.Max(chaProperty1.maxCost, -0.9999f))
            
        ));
    }
}
