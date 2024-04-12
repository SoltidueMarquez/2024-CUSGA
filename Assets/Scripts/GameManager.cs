using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    
    /// <summary>
    /// �ȰѼ��س���д���������Ҫ�޸�
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        //���س���
        SceneManager.LoadScene(sceneName);
    }
    
}
