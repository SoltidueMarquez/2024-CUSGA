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
        //����н����еĶԾ֣����������Ϸ��ť

        settingsButton.onClick.AddListener(SettingsManager.Instance.OnClickSettingsButton);
    }

    public void OnClickStartButton()
    {
        //����н����еĶԾ֣���ʾ��ʾ
        StartNewGame();
    }

    public void OnClickContinueButton()
    {
        //���ش浵����ת����
        SceneManager.LoadScene("SampleScene");
    }

    private void StartNewGame()
    {
        //��մ浵

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

