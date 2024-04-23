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
        //����н����еĶԾ֣����������Ϸ��ť
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
        //����н����еĶԾ֣���ʾ��ʾ
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
        //���ش浵����ת����
        SceneManager.LoadScene("SampleScene");
    }

    public void StartNewGame()
    {
        //��մ浵
        GameManager.Instance.NewGame();
        //���ص�ͼ
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

