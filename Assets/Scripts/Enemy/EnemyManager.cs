using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager
{
    /// <summary>
    /// ���ݵ��˵����ͻ�ȡ���˵�����
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
