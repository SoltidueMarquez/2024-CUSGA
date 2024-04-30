using DesignerScripts;
using System.Collections.Generic;
using UnityEngine;
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
    Easy,
    Normal,
    Hard,
    Boss
}
[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Enemy/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [Header("EnemyID")]
    public string EnemyID;
    [Header("敌人的类型")]
    public EnemyType enemyType;
    [Header("敌人初始时候身上的骰子列表")]
    public List<DiceSOItem> EnemyBattleDiceList;
    [Header("敌人初始的数值")]
    public ChaProperty baseProp;
    [Header("敌人初始的buff")]
    public List<BuffDataConfig> enemyBuffs;
    [Header("敌人的sprite")]
    public Sprite enemySprite;
    [Header("敌人的背景sprite")]
    public Sprite enemyBackgroundSprite;
}
    //AI暂时不管