using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Enemy/EnemyDataSO")]
[System.Serializable]
public class BuffDataConfig
{
    //敌人的buff配置
    [Header("buff的类型")]
    public BuffDataSO buffDataSO;
    [Header("buff的叠加次数")]
    public int buffStack = 1;
    [Header("buff的持续回合")]
    public int buffRound = 0;
}
public enum EnemyType
{
    //敌人的类型
    Normal,
    Elite,
    Boss
}
public class EnemyDataSO : ScriptableObject
{
    [Header("敌人的类型")]
    public EnemyType enemyType;
    [Header("敌人初始时候身上的骰子列表")]
    public List<DiceSOItem> EnemyBattleDiceList;
    [Header("敌人初始的数值")]
    public ChaProperty baseProp;
    [Header("敌人初始的buff")]
    public List<BuffDataConfig> enemyBuffs;
}
    //AI暂时不管