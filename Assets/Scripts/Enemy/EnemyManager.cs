using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EnemyManager
{
    /// <summary>
    /// 根据敌人的类型获取敌人的数据
    /// </summary>
    /// <param name="enemyType"></param>
    /// <returns></returns>
    public static EnemyDataSO GetEnemyDataSOviaCondition(EnemyType enemyType,List<string> enemyIDs)
    {
        EnemyDataSO[] enemyDataSOs = Resources.LoadAll<EnemyDataSO>("Enemy");
        //这边先找出所有的敌人，然后再找出符合条件的敌人
        List<EnemyDataSO> resultList = new();
        for (int i = 0; i < enemyDataSOs.Length; i++)
        {
            if (enemyDataSOs[i].enemyType == enemyType && !enemyIDs.Contains(enemyDataSOs[i].EnemyID))
            {
                resultList.Add(enemyDataSOs[i]);
            }
        }
        //var resultList = enemyDataSOs.Where(x => ((!enemyIDs.Contains(x.EnemyID)) && x.enemyType == enemyType)).ToList();
        Debug.Log("找到的敌人数量" + resultList.Count);

        if(resultList.Count == 0)
        {
            Debug.LogWarning("找不到符合要求的新敌人，将从旧的中随机挑选");
            resultList = enemyDataSOs.Where(x => x.enemyType == enemyType).ToList();
        }
        
        return resultList[Random.Range(0,resultList.Count)];
    }
}
