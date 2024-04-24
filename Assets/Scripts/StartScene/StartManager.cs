using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoSingleton<StartManager>
{
    public Button settingsButton;
    public GameObject continueButton;
    public GameObject checkCanvas;
    // Start is called before the first frame update
    void Start()
    {
        checkCanvas.SetActive(false);
        //如果有进行中的对局，激活继续游戏按钮
        if (GameManager.Instance.CheckIfHasSaveData())
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }

        settingsButton.onClick.AddListener(SettingsManager.Instance.OnClickSettingsButton);
    }

    public void OnClickStartButton()
    {
        //如果有进行中的对局，显示提示
        if (GameManager.Instance.CheckIfHasSaveData())
        {
            checkCanvas.SetActive(true);
        }
        else
        {
            StartNewGame();
        }
    }

    public void OnClickContinueButton()
    {
        GameManager.Instance.ContinueGame();
        //加载存档，跳转场景
        SceneManager.LoadScene("MapScene");
    }

    public void StartNewGame()
    {
        //清空存档
        GameManager.Instance.NewGame();
        //加载地图
        SceneManager.LoadScene("MapScene");
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

