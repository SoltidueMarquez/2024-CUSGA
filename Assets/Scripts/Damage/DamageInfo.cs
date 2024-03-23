using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏中任何一次伤害，治疗逻辑，都会产生一条damageInfo,由此开始正常的伤害计算
/// </summary>
public class DamageInfo
{
    public GameObject attacker;

    public GameObject defender;

    //TODO:伤害类型?
    /// <summary>
    /// 最简单的伤害数据
    /// </summary>
    public Damage damage;
    /// <summary>
    /// 伤害可能产生的buff,这边需要在骰面里面或者每次OnHit的时候
    /// </summary>
    public List<BuffInfo> addBuffs = new List<BuffInfo>();
    /// <summary>
    /// 骰子的类型,决定了具体逻辑计算方式
    /// </summary>
    public DiceType diceType;
    /// <summary>
    /// 使用公式计算完的最终伤害
    /// </summary>
    public int finalDamage = 0;
    /// <summary>
    /// 增伤区
    /// </summary>
    public float addDamageArea = 0;
    /// <summary>
    /// 减伤区
    /// </summary>
    public float reduceDamageArea = 0;
    /// <summary>
    /// 伤害等级
    /// </summary>
    public int level;



    public DamageInfo(GameObject attacker, GameObject defender, Damage damage, DiceType diceType, int level, List<BuffInfo> buffInfos)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.damage = damage;
        this.diceType = diceType;
        if (buffInfos != null)
        {
            addBuffs.AddRange(buffInfos);
        }
    }

}
/// <summary>
/// 伤害类型,暂时用不到
/// </summary>
public enum DamageType
{
    Physical,
    Magic,
    True
}
