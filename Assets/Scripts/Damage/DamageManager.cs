using System.Collections;
using System.Collections.Generic;
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
        while(damageInfos.Count > 0)
        {
            DealWithDamage(damageInfos.Peek());
            damageInfos.Dequeue();
        }
    }
    public void DealWithDamage(DamageInfo damageInfo)
    {
        if (!damageInfo.defender) return;
        ChaState attackerChaState = damageInfo.attacker.GetComponent<ChaState>();
        ChaState defenderChaState = damageInfo.defender.GetComponent<ChaState>();
        foreach (var buff in attackerChaState.GetBuffHandler().buffList)
        {
            buff.buffData.onHit?.Invoke(buff, damageInfo, damageInfo.defender);
        }
        foreach (var buff in defenderChaState.GetBuffHandler().buffList)
        {
            buff.buffData.onBeHurt?.Invoke(buff, damageInfo, damageInfo.attacker);
        }
        //如果能被杀死，就会走OnKill和OnBeKilled
        if(defenderChaState.CanBeKilledByDamageInfo(damageInfo))
        {
            if(attackerChaState != null)
            {
                foreach(var buff in attackerChaState.GetBuffHandler().buffList)
                {
                    buff.buffData.onKill?.Invoke(buff, damageInfo, damageInfo.defender);
                }
            }
            foreach(var buff in defenderChaState.GetBuffHandler().buffList)
            {
                buff.buffData.onBeKilled?.Invoke(buff, damageInfo, damageInfo.attacker);
            }
        }
        if (damageInfo.isHeal)
        {
            defenderChaState.ModResources(new ChaResource(damageInfo.finalDamage));
        }
        else
        {
            defenderChaState.ModResources(new ChaResource(-damageInfo.finalDamage));
        }
        //TODO:视觉上的变化
        //伤害流程走完，添加buff
        for(int i = 0;i<damageInfo.addBuffs.Count;i++)
        {
            GameObject toCha = damageInfo.addBuffs[i].target;
            ChaState toChaState = toCha.Equals(damageInfo.attacker) ? attackerChaState : defenderChaState;
            toChaState.GetBuffHandler().AddBuff(damageInfo.addBuffs[i], damageInfo.attacker);
        }
        
    }



    public void DoDamage(GameObject attacker, GameObject target, Damage damage, bool isHeal)
    {
        damageInfos.Enqueue(new DamageInfo(attacker, target, damage, isHeal));
    }
}
