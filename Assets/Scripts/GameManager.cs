using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 整个游戏的管理类,一些整体的管理都在这边
/// </summary>

public class GameManager : MonoSingleton<GameManager>
{
    public EnemyDataSO enemyDataSO;//用于传递敌人数据
    [HideInInspector]
    public bool ifLoadedHalidom;//用于判断是否加载了圣物,全局只加载一次，因为圣物是全局的
    [HideInInspector]
    public string currentMap;
    [Header("玩家数据")]
    public PlayerDataSO playerDataSO;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    #region 开始场景调用
    public void NewGame()
    {
        playerDataSO.ifUseSaveData = false;//这样就会在进入地图的时候重新生成地图
    }
    public void ContinueGame()
    {
        playerDataSO.ifUseSaveData = true;
    }
    public bool CheckIfHasSaveData()
    {
        playerDataSO.LoadData();
        return playerDataSO.ifHasData;
    }
    #endregion
    #region 结算场景调用
    public int GetPlayerKillEnemyCount()
    {
        return playerDataSO.playerRoomData.enemyIDs.Count;
    }

    public int GetCurrentPlayerLayer()
    {
        return playerDataSO.playerRoomData.roomNums;
    }
    public int GetPlayerKillBossCount()
    {
        return playerDataSO.playerRoomData.bossIDs.Count;
    }
    public int GetHalidomCount()
    {
        return playerDataSO.halidomDataForSaves.Count;
    }
    public int GetBagDiceCount()
    {
        return playerDataSO.bagDiceList.Count;
    }
    public int GetExtraMoney()
    {
        return playerDataSO.chaResource.currentMoney - playerDataSO.baseProp.money;
    }
    /// <summary>
    /// 检查是否通关，暂时只有一个boss，做成这样是为了以后扩展
    /// </summary>
    /// <returns></returns>
    public bool CheckIfPassGame()
    {
        return playerDataSO.playerRoomData.bossIDs.Count > 0;
    }
    #endregion

}
