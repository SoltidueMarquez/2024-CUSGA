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
    [HideInInspector]
    public EnemyDataSO enemyDataSO;//用于传递敌人数据
    [HideInInspector]
    public bool ifLoadedHalidom;//用于判断是否加载了圣物,全局只加载一次，因为圣物是全局的
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
        return playerDataSO.enemyIDList.Count;
    }

    public int GetCurrentPlayerLayer()
    {
        return playerDataSO.enemyIDList.Count;
    }
    #endregion

}
