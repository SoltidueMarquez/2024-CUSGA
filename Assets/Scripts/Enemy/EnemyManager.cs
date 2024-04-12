using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager
{
    /// <summary>
    /// 根据敌人的类型获取敌人的数据
    /// </summary>
    /// <param name="enemyType"></param>
    /// <returns></returns>
    public static EnemyDataSO GetEnemyDataSOviaCondition(EnemyType enemyType)
    {
        EnemyDataSO[] enemyDataSOs = Resources.LoadAll<EnemyDataSO>("Enemy");
        foreach (EnemyDataSO enemyDataSO in enemyDataSOs)
        {
            if (enemyDataSO.enemyType == enemyType)
            {
                return enemyDataSO;
            }
        }
        Debug.LogWarning("ResourcesManager:EnemyDataSO is null");
        return null;
    }
}
