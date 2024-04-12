using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
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

    
}
