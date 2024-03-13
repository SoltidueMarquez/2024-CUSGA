using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [SerializeField]
    private List<DamageInfo> damageInfos = new List<DamageInfo>();

    public void DealWithDamage(DamageInfo damageInfo)
    {
        if(!damageInfo.defender) return;
        
    }



    public void AddDamageInfo(DamageInfo damageInfo)
    {
        damageInfos.Add(damageInfo);
    }
}
