using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public enum GameScene
{
    MapScene,
    BattleScene,

}
public class SceneLoader:MonoSingleton<SceneLoader>
{
    public Animator animator;
    public static async UniTask LoadSceneAsyncUnitask(GameScene gameScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameScene.ToString(),LoadSceneMode.Single);
        await UniTask.WaitUntil(() => asyncLoad.isDone);
        Debug.Log("Scene Loaded");
    }

    public static async void LoadSceneAsync(GameScene gameScene)
    {
        
        await LoadSceneAsyncUnitask(gameScene);
    }
}
