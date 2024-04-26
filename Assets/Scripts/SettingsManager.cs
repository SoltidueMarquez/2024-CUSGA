using Audio_Manager;
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
        gameSpeedToggle.isOn = (PlayerPrefs.GetInt("GameSpeedToggle") == 1) ? true : false;
        OnClickSpeedToggle(gameSpeedToggle.isOn);
        Time.timeScale = gameSpeed;

        mainVolume.onValueChanged.AddListener(OnMainVolumeChanged);
        musicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundVolume.onValueChanged.AddListener(OnSoundVolumeChanged);

        mainVolume.value = PlayerPrefs.GetFloat(VolumeSavingKeys.MainVolume.ToString());
        musicVolume.value = PlayerPrefs.GetFloat(VolumeSavingKeys.MusicVolume.ToString());
        soundVolume.value = PlayerPrefs.GetFloat(VolumeSavingKeys.SoundVolume.ToString());

        if (PlayerPrefs.GetInt("FirstOpen") == 0)
        {
            PlayerPrefs.SetInt("FirstOpen", 1);
            mainVolume.value = 1;
            musicVolume.value = 1;
            soundVolume.value = 1;
        }

        mainVolume.onValueChanged?.Invoke(mainVolume.value);
        musicVolume.onValueChanged?.Invoke(musicVolume.value);
        soundVolume.onValueChanged?.Invoke(soundVolume.value);

        CloseSettings();
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

        // Time.timeScale = 0f;
    }

    private void CloseSettings()
    {
        SettingsCanvas.SetActive(false);
        FreezeMap(false);

        PlayerPrefs.SetFloat(VolumeSavingKeys.MainVolume.ToString(), mainVolume.value);
        PlayerPrefs.SetFloat(VolumeSavingKeys.SoundVolume.ToString(), soundVolume.value);
        PlayerPrefs.SetFloat(VolumeSavingKeys.MusicVolume.ToString(), musicVolume.value);

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

    #region------音量控制------

    public Scrollbar mainVolume;
    public Scrollbar musicVolume;
    public Scrollbar soundVolume;

    private void OnMainVolumeChanged(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }

    private void OnSoundVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetSfxVolume(value);
    }

    #endregion

    [Button]
    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

public enum VolumeSavingKeys
{
    MainVolume,
    MusicVolume,
    SoundVolume
}
