using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Enemy/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [Header("敌人初始时候身上的骰子列表")]
    public List<DiceSOItem> EnemyBattleDiceList;
}
