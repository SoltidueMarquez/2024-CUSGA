using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ChaControlState
{
    /// <summary>
    /// 是否可以移动背包骰面和战斗骰面的位置
    /// </summary>
    public bool ifCanChangePosition;
    /// <summary>
    /// 是否可以使用骰面
    /// </summary>
    public bool canUseDice;
    /// <summary>
    /// 是否无敌
    /// </summary>
    public bool ifImmune;

    public ChaControlState(bool ifCanChangePosition = true, bool canUseDice = true, bool ifImmune = false)
    {
        this.ifCanChangePosition = ifCanChangePosition;
        this.canUseDice = canUseDice;
        this.ifImmune = ifImmune;
    }

    public void Origin()
    {
        ifCanChangePosition = true;
        canUseDice = true;
        ifImmune = false;
    }
    public static ChaControlState origin = new ChaControlState(true, true, false);
    public static ChaControlState operator +(ChaControlState a, ChaControlState b)
    {
        return new ChaControlState(a.ifCanChangePosition && b.ifCanChangePosition, a.canUseDice && b.canUseDice, a.ifImmune || b.ifImmune);
    }
    //TODO:重载运算符，用于状态的合并，以及一些static的默认状态
}
