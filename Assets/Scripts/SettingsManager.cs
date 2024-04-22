using Map;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoSingleton<SettingsManager>
{
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public GameObject SettingsCanvas;
    public GameObject backToStartButton;
    public Toggle gameSpeedToggle;
    public float gameSpeed = 1;

    void Start()
    {
        CloseSettings();
        gameSpeedToggle.isOn = (PlayerPrefs.GetInt("GameSpeedToggle") == 1) ? true : false;
        OnClickSpeedToggle(false);
        Time.timeScale = gameSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPressEsc();
        }
    }

    public void OnClickSettingsButton()
    {
        OnPressEsc();
    }

    private void OnPressEsc()
    {
        if (SettingsCanvas.activeSelf)
        {
            CloseSettings();
        }
        else
        {
            OpenSettings();
        }
    }

    private void OpenSettings()
    {
        SettingsCanvas.SetActive(true);
        FreezeMap(true);

        if (SceneManager.GetActiveScene().name == "StartGame")
        {
            backToStartButton.SetActive(false);
        }
        else
        {
            backToStartButton.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    private void CloseSettings()
    {
        SettingsCanvas.SetActive(false);
        FreezeMap(false);

        Time.timeScale = gameSpeed;
    }

    ScrollNonUI scrollNonUI;
    /// <summary>
    /// 调整地图状态，true为冻结，false为解冻
    /// </summary>
    /// <param name="ifFreeze"></param>
    public void FreezeMap(bool freezeState)
    {

        if (scrollNonUI == null)
        {
            if (GameObject.Find("MapParentWithAScroll") == null)
            {
                return;
            }
            scrollNonUI = GameObject.Find("MapParentWithAScroll").GetComponent<ScrollNonUI>();
        }

        if (freezeState)
        {
            scrollNonUI.freezeX = true;
        }
        else
        {
            scrollNonUI.freezeX = false;
        }
    }

    public void OnClickBackToStart()
    {
        SceneManager.LoadScene("StartGame");
        CloseSettings();
    }

    public void OnClickSpeedToggle(bool toggle)
    {
        if (gameSpeedToggle.isOn)
        {
            PlayerPrefs.SetInt("GameSpeedToggle", 1);
            gameSpeed = 2;
        }
        else
        {
            PlayerPrefs.SetInt("GameSpeedToggle", 0);
            gameSpeed = 1;
        }
        PlayerPrefs.Save();
        //Debug.Log("OnClickSpeedToggle   " + gameSpeed);
    }

    [Button]
    public void ShowSpeed()
    {
        Debug.Log("游戏速度： " + Time.timeScale);
    }
}
