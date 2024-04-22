using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoSingleton<StartManager>
{
    public Button settingsButton;
    // Start is called before the first frame update
    void Start()
    {
        //如果有进行中的对局，激活继续游戏按钮

        settingsButton.onClick.AddListener(SettingsManager.Instance.OnClickSettingsButton);
    }

    public void OnClickStartButton()
    {
        //如果有进行中的对局，显示提示
        StartNewGame();
    }

    public void OnClickContinueButton()
    {
        //加载存档，跳转场景
        SceneManager.LoadScene("SampleScene");
    }

    private void StartNewGame()
    {
        //清空存档

        //加载地图
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickQuitButton()
    {
        
        PlayerPrefs.Save();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

