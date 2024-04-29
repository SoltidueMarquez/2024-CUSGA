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
    StartGame,
    TurtorialMap,
    TurtorialBattle,
}
public enum TransitionMode
{
    In,
    Out
}
public enum SceneTransition
{
    crossInCrossOut,
    maskInMaskOut,
}

public class SceneLoader : MonoSingleton<SceneLoader>
{
    [Header("组件")]
    public Animator animator;
    public GameObject canvas;
    public Image maskImage;
    private Material maskMaterial;
    [Header("参数")]
    public float duration;
    public GameScene currentGameScene;
    private CanvasGroup canvasGroup;

    private int appearHash = Animator.StringToHash("Appear");
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        maskMaterial = maskImage.material;
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        Hide();

    }


    public async void LoadSceneAsync(GameScene gameScene, Vector2 position,SceneTransition sceneTransition)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameScene.ToString(), LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        switch (sceneTransition)
        {
            case SceneTransition.crossInCrossOut:
                await FadeInFadeOutTranstion(TransitionMode.In);
                asyncLoad.allowSceneActivation = true;
                currentGameScene = gameScene;
                await FadeInFadeOutTranstion(TransitionMode.Out);
                break;
            case SceneTransition.maskInMaskOut:
                await MaskTranstion(position, duration, TransitionMode.In);
                asyncLoad.allowSceneActivation = true;
                currentGameScene = gameScene;
                await MaskTranstion(new Vector2(0.5f, 0.5f), duration, TransitionMode.Out);
                break;
        }
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
    #region 淡入淡出
    public async UniTask FadeInFadeOutTranstion(TransitionMode transitionMode)
    {
        Show();
        if (transitionMode == TransitionMode.In)
        {
            animator.SetBool(appearHash, true);
            await UniTask.WaitUntil(() => IsCurrentAnimationFinished("Fadein"));
        }
        else
        {
            animator.SetBool(appearHash,false);
            await UniTask.WaitUntil(() => IsCurrentAnimationFinished("Fadeout"));
        }
        Hide();
    }
    private bool IsCurrentAnimationFinished(string animationName)
    {
        // 获取当前动画状态
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 检查当前动画片段是否播放完毕
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }
    #endregion
    #region 显示隐藏
    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;
    }
    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    #endregion
}
