using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct Damage
{
    public int baseDamage;
    public float indexDamageRate;
    public Damage(int baseDamage,float indexDamageRate)
    {
        this.baseDamage = baseDamage;
        this.indexDamageRate = indexDamageRate;
    }


    public static Damage operator +(Damage a, Damage b)
    {
        return new Damage(a.baseDamage + b.baseDamage,a.indexDamageRate + b.indexDamageRate);
    }
    /// <summary>
    ///最终的伤害在这边留一个口子，可以供策划进行一些最终伤害的计算
    /// </summary>
    /// <param name="damage">用于计算最终伤害的damage</param>
    /// <param name="level">骰面当前的等级</param>
    /// <param name="diceType">骰子的种类，决定是哪种类型的数值</param>
    /// <returns></returns>
    public static int FinalDamage(Damage damage, int level,DiceType diceType)
    {
        //这边可以加入一些策划的计算,先向下取整
        int baseDamage = DamageUtil.GetlevelBasedDamage(level);
        return Mathf.FloorToInt((baseDamage ) * (1+damage.indexDamageRate * 0.1f));
    }
}

