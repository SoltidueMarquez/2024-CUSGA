using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System;

public enum GameScene
{
    MapScene,
    BattleScene,
    StartGame
}
public enum TransitionMode
{
    In,
    Out
}

public class SceneLoader : MonoSingleton<SceneLoader>
{
    public Animator animator;
    public GameObject canvas;
    public Image maskImage;
    private Material maskMaterial;
    [Header("参数")]
    public float duration;
    public GameScene currentGameScene;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        maskMaterial = maskImage.material;
        Hide();

    }


    public async void LoadSceneAsync(GameScene gameScene, Vector2 position)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameScene.ToString(), LoadSceneMode.Single);

        asyncLoad.allowSceneActivation = false;
        await MaskTranstion(position, duration, TransitionMode.In);
        asyncLoad.allowSceneActivation = true;
        currentGameScene = gameScene;
        await MaskTranstion(new Vector2(0.5f,0.5f), duration, TransitionMode.Out);
        Debug.Log(currentGameScene.ToString());
    }

    #region 遮罩转场
    public async UniTask MaskTranstion(Vector2 position, float duration, TransitionMode transitionMode)
    {
        Show();
        float timer = 0;
        //设置遮罩的位置
        maskMaterial.SetFloat("_PositionX", position.x);
        maskMaterial.SetFloat("_PositionY", position.y);
        //把duration换算一下，从秒换算到毫秒
        float newDuration = duration * 1000;
        //设置遮罩的大小
        //根据时间从0-1变化
        while (timer < newDuration)
        {
            if (transitionMode == TransitionMode.Out)
            {
                maskMaterial.SetFloat("_Radius", timer / newDuration * 2);
                if (maskMaterial.GetFloat("_Radius") > 1.9f)
                {
                    maskMaterial.SetFloat("_Radius", 2);
                }
            }
            else
            {
                maskMaterial.SetFloat("_Radius", 2 - (timer / newDuration * 2));
                if (maskMaterial.GetFloat("_Radius") < 0.1f)
                {
                    maskMaterial.SetFloat("_Radius", 0);
                }
            }
            timer += 20;
            await UniTask.Delay(TimeSpan.FromMilliseconds(20));
        }
        Hide();
    }
    #endregion

    #region 转场基础函数
    public void Show()
    {
        canvas.SetActive(true);
    }
    public void Hide()
    {
        canvas.SetActive(false);
    }
    #endregion
}
