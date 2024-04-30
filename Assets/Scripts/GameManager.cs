using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 整个游戏的管理类,一些整体的管理都在这边
/// </summary>

public class GameManager : MonoBehaviour
{
    public EnemyDataSO enemyDataSO;//用于传递敌人数据
    [HideInInspector]
    public bool ifLoadedHalidom;//用于判断是否加载了圣物,全局只加载一次，因为圣物是全局的
    [HideInInspector]
    public string currentMap;
    [Header("编辑器中的玩家数据")]
    public PlayerDataSO playerDataSOTemplate;
    [Header("玩家数据")]
    public PlayerDataSO playerDataSO;
    [Header("是否是新手教程")]
    public bool ifTutorial;
    public bool mapTurtorial;
    public bool battleTurtorial;
    public bool storeTurtorial;
    public bool rewardTurtorial;
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        Debug.Log("Awake");
        playerDataSO = ScriptableObject.CreateInstance<PlayerDataSO>();
        playerDataSO.InitPlaydataSOInstance(playerDataSOTemplate);
        //ifFirstEnterGame();
    }
    void Start()
    {
    }

    #region 开始场景调用
    public void NewGame()
    {
        //playerDataSO.ifUseSaveData = false;//这样就会在进入地图的时候重新生成地图
        playerDataSO = ScriptableObject.CreateInstance<PlayerDataSO>();
        Debug.Log("NewGame");
        playerDataSO.InitPlaydataSOInstance(playerDataSOTemplate);
    }
    public void ContinueGame()
    {
        //playerDataSO.LoadData();
        playerDataSO.ifUseSaveData = true;
    }
    public bool CheckIfHasSaveData()
    {
        if (playerDataSO == null) return false;

        playerDataSO.LoadData();
        return playerDataSO.ifHasData;
    }
    //判断是否是第一次进入游戏
    public void ifFirstEnterGame()
    {
        if (PlayerPrefs.HasKey("FirstEnterGame"))
        {
            this.ifTutorial = false;
            PlayerPrefs.SetInt("FirstEnterGame", 1);
        }
        else
        {
            this.ifTutorial = true;
        }
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
