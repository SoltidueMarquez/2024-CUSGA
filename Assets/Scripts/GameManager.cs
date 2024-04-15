using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 整个游戏的管理类,一些整体的管理都在这边
/// </summary>
public enum GameScene
{
    MapScene,
    BattleScene,
    
}
public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector]
    public EnemyDataSO enemyDataSO;//用于传递敌人数据
    [HideInInspector]
    public bool ifLoadedHalidom;//用于判断是否加载了圣物,全局只加载一次，因为圣物是全局的
    

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    /// <summary>
    /// 先把加载场景写在这里，还需要修改
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    }
    
}
