using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Enemy/EnemyDataSO")]
[System.Serializable]
public class EnemyBuffDataConfig
{
    //敌人的buff配置
    [Header("buff的类型")]
    public BuffDataSO buffDataSO;
    [Header("buff的叠加次数")]
    public int buffStack;
    [Header("buff的持续回合")]
    public int buffRound;
}
public class EnemyDataSO : ScriptableObject
{
    [Header("敌人的类型")]
    [Header("敌人初始时候身上的骰子列表")]
    public List<DiceSOItem> EnemyBattleDiceList;
    [Header("敌人初始的数值")]
    public ChaProperty baseProp;
    [Header("敌人初始的buff")]
    public List<EnemyBuffDataConfig> enemyBuffs;
}
    //AI暂时不管