using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
/// <summary>
/// ������Ϸ�Ĺ�����,һЩ����Ĺ��������
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
