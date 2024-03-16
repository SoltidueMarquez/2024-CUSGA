using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色的资源类属性，包括生命值，金钱，重投次数，护盾
/// </summary>
public class ChaResource
{
    /// <summary>
    /// 玩家当前的生命值
    /// </summary>
    public int currentHp;
    /// <summary>
    /// 玩家当前的金钱
    /// </summary>
    public int currentMoney;
    /// <summary>
    /// 玩家当前的重投次数
    /// </summary>
    public int currentRollTimes;
    /// <summary>
    /// 玩家当前的护盾
    /// </summary>
    public int currentShield;

    public ChaResource(int currentHp = 0, int currentMoney = 0, int currentRollTimes = 0, int currentShield = 0)
    {
        this.currentHp = currentHp;
        this.currentMoney = currentMoney;
        this.currentRollTimes = currentRollTimes;
        this.currentShield = currentShield;
    }

    public static ChaResource zero = new ChaResource(0, 0, 0, 0);

    public static ChaResource operator +(ChaResource a, ChaResource b)
    {
        return new ChaResource(a.currentHp + b.currentHp, a.currentMoney + b.currentMoney, a.currentRollTimes + b.currentRollTimes, a.currentShield + b.currentShield);
    }
    public static ChaResource operator -(ChaResource a, ChaResource b)
    {
        return new ChaResource(a.currentHp - b.currentHp, a.currentMoney - b.currentMoney, a.currentRollTimes - b.currentRollTimes, a.currentShield - b.currentShield);
    }
    public static ChaResource operator *(ChaResource a, int b)
    {
        return new ChaResource(a.currentHp * b, a.currentMoney * b, a.currentRollTimes * b, a.currentShield * b);

    }
    public static ChaResource operator *(int a, ChaResource b)
    {
        return b * a;
    }
    public bool Enough(ChaResource cost)
    {
        return currentHp >= cost.currentHp && currentMoney >= cost.currentMoney && currentRollTimes >= cost.currentRollTimes && currentShield >= cost.currentShield;
    }
}
