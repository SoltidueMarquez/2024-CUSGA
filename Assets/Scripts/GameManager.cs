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
    public EnemyDataSO enemyDataSO;
    

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
        //加载场景
        SceneManager.LoadScene(sceneName);
    }
    
}
