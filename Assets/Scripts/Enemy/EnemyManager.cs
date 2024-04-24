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
        var resultList = enemyDataSOs.Where(x => ((!enemyIDs.Contains(x.EnemyID)) && x.enemyType == enemyType)).ToList();
        if(resultList.Count == 0)
        {
            Debug.LogWarning("找不到符合要求的新敌人，将从旧的中随机挑选");
            resultList = enemyDataSOs.Where(x => x.enemyType == enemyType).ToList();
        }
        
        return resultList[Random.Range(0,resultList.Count)];
    }
}
