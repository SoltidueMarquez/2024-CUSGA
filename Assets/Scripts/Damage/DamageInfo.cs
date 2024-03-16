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

    public bool isHeal;
    //TODO:伤害类型?
    /// <summary>
    /// 最简单的伤害数据
    /// </summary>
    public Damage damage;
    /// <summary>
    /// 伤害可能产生的buff
    /// </summary>
    public List<BuffInfo> addBuffs = new List<BuffInfo>();

    public DamageInfo(GameObject attacker, GameObject defender, Damage damage, bool isHeal = false)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.damage = damage;
        this.isHeal = isHeal;
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
