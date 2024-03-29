using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class DamageManager : MonoSingleton<DamageManager>
{
    [SerializeField]
    private Queue<DamageInfo> damageInfos = new Queue<DamageInfo>();
    /// <summary>
    /// 对当前所有的伤害数据进行处理
    /// </summary>
    public void DealWithAllDamage()
    {
        //如果要配合视觉效果，可以在这里添加一个协程，每次处理一个伤害
        while (damageInfos.Count > 0)
        {
            DealWithDamage(damageInfos.Peek());
            //Debug.Log(damageInfos.Peek().finalDamage;
            damageInfos.Dequeue();
        }
    }
    public void DealWithDamage(DamageInfo damageInfo)
    {
        if (!damageInfo.defender) return;
        ChaState attackerChaState = damageInfo.attacker.GetComponent<ChaState>();
        ChaState defenderChaState = damageInfo.defender.GetComponent<ChaState>();
        damageInfo.damage.SetBaseDamage(damageInfo.level, damageInfo.diceType);
        //这边先执行所有的圣物的onhit
        if (attackerChaState.side == 0)
        {
            foreach (var halidom in HalidomManager.Instance.halidomList)
            {
                foreach (var buff in halidom.buffInfos)
                {
                    buff.buffData.onHit?.Invoke(buff, damageInfo, damageInfo.defender);
                }
            }
        }

        foreach (var buff in attackerChaState.GetBuffHandler().buffList)
        {
            buff.buffData.onHit?.Invoke(buff, damageInfo, damageInfo.defender);
        }
        foreach (var buff in defenderChaState.GetBuffHandler().buffList)
        {
            buff.buffData.onBeHurt?.Invoke(buff, damageInfo, damageInfo.attacker);
        }
        //如果能被杀死，就会走OnKill和OnBeKilled
        if (defenderChaState.CanBeKilledByDamageInfo(damageInfo))
        {
            if (attackerChaState != null)
            {
                foreach (var buff in attackerChaState.GetBuffHandler().buffList)
                {
                    buff.buffData.onKill?.Invoke(buff, damageInfo, damageInfo.defender);
                }
            }
            foreach (var buff in defenderChaState.GetBuffHandler().buffList)
            {
                buff.buffData.onBeKilled?.Invoke(buff, damageInfo, damageInfo.attacker);
            }
        }
        damageInfo.finalDamage = Damage.FinalDamage(damageInfo.damage, damageInfo.level, damageInfo.diceType, damageInfo.addDamageArea, damageInfo.reduceDamageArea);
        Debug.Log("攻击伤害：" + damageInfo.finalDamage);

        //根据类型不同执行不同的逻辑
        //这边先确定是对玩家还是敌人
        Character character = defenderChaState.side == 0 ? Character.Player : Character.Enemy;
        Character attackCharacter = attackerChaState.side == 0 ? Character.Player : Character.Enemy;
        switch (damageInfo.diceType)
        {
            case DiceType.Attack:
                defenderChaState.ModResources(new ChaResource(-damageInfo.finalDamage, 0, 0, 0));
                CharacterUIManager.Instance.Attack(attackCharacter);
                CharacterUIManager.Instance.BeAttacked(character, damageInfo.finalDamage);
                break;
            case DiceType.Defense:
                //这边暂时做成只对玩家生效
                attackerChaState.ModResources(new ChaResource(0, 0, 0, damageInfo.finalDamage));
                CharacterUIManager.Instance.UseOtherDice(attackCharacter);
                break;
            case DiceType.Support:
                attackerChaState.ModResources(new ChaResource(damageInfo.finalDamage, 0, 0, 0));
                CharacterUIManager.Instance.UseOtherDice(attackCharacter);
                break;
        }
        //TODO:视觉上的变化
        //伤害流程走完，添加buff
        for (int i = 0; i < damageInfo.addBuffs.Count; i++)
        {
            ChaState toChaState = defenderChaState;
            toChaState.GetBuffHandler().AddBuff(damageInfo.addBuffs[i], damageInfo.attacker);
        }

    }



    public void DoDamage(DamageInfo damageInfo)
    {
        damageInfos.Enqueue(damageInfo);
    }
}
