using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSetting : MonoBehaviour
{
    bool isActive;
    RectTransform mRect;

    public TMP_Dropdown mDropdownLanguage;
    public TMP_Dropdown mDropdownWindow;
    public Toggle mToggleWindow;
    public Slider mMasterVolume;
    public Slider mBGMVolume;
    public Slider mSFXVolume;

    public TMP_Text mTextMasterVolume;
    public TMP_Text mTextBGMVolume;
    public TMP_Text mTextSFXVolume;

    public TMP_Text[] mText;

    public void UpdateText()
    {
        for(int i=0; i<mText.Length; ++i)
        {
            mText[i].text = GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].HUDSetting[i];
        }
    }
    void InitLanguage()
    {
        for (int i = 0; i < GameManager.instance.mTextJsonData.Length; ++i)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();

            option.text = GameManager.instance.mTextJsonData[i].Language;
            mDropdownLanguage.options.Add(option);
        }
        mDropdownLanguage.RefreshShownValue();
        mDropdownLanguage.value = (int)GameManager.instance.mSettingData.LanguageType;
    }
    void InitWindow()
    {
        for (int i = 0; i < Screen.resolutions.Length; ++i)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = Screen.resolutions[i].width + "x " + Screen.resolutions[i].height + " " + Screen.resolutions[i].refreshRate + "hz";
            mDropdownWindow.options.Add(option);
        }
        mDropdownWindow.RefreshShownValue();
        mDropdownWindow.value = GameManager.instance.mSettingData.ResolutionNum;
        mToggleWindow.isOn = GameManager.instance.mSettingData.Fullscreen;
    }
    void InitVolume()
    {
        mMasterVolume.value = GameManager.instance.mSettingData.MasterVolume;
        mBGMVolume.value = GameManager.instance.mSettingData.BGMVolume;
        mSFXVolume.value = GameManager.instance.mSettingData.SFXVolume;
    }

    public void Init()
    {
        InitLanguage();
        InitWindow();
        InitVolume();
    }
    private void Awake()
    {
        mRect = GetComponent<RectTransform>();
        isActive = false;
        Init();
    }

    public void Show()
    {
        Time.timeScale = 0;
        isActive = true;
        mRect.localScale = Vector3.one;

        GameManager.instance.PlayEffect(true);
    }

    public void Hide()
    {
        Time.timeScale = 1;
        isActive = false;
        mRect.localScale = Vector3.zero;

        GameManager.instance.PlayEffect(false);
    }

    public void Toggle()
    {
        if (isActive)
            Hide();
        else
            Show();
    }

    public void ToTitle()
    {
        GameManager.instance.SaveSettingJson();
        GameManager.instance.GameRetry();
    }

    public void SetLanguage()
    {
        GameManager.instance.mSettingData.LanguageType = (Enum.Language)mDropdownLanguage.value;

        GameManager.instance.UpdateText();
    }
    public void SetWindowScreen()
    {
        GameManager.instance.mSettingData.ResolutionNum = mDropdownWindow.value;
        GameManager.instance.mSettingData.Width = Screen.resolutions[mDropdownWindow.value].width;
        GameManager.instance.mSettingData.Height = Screen.resolutions[mDropdownWindow.value].height;
        GameManager.instance.mSettingData.RefreshRate = Screen.resolutions[mDropdownWindow.value].refreshRate;

        Screen.SetResolution(GameManager.instance.mSettingData.Width, GameManager.instance.mSettingData.Height, GameManager.instance.mSettingData.Fullscreen, GameManager.instance.mSettingData.RefreshRate);
    }
    public void SetFullscreen()
    {
        GameManager.instance.mSettingData.Fullscreen = mToggleWindow.isOn;

        Screen.SetResolution(GameManager.instance.mSettingData.Width, GameManager.instance.mSettingData.Height, GameManager.instance.mSettingData.Fullscreen, GameManager.instance.mSettingData.RefreshRate);
    }
    public void SetMasterVolume()
    {
        GameManager.instance.mSettingData.MasterVolume = mMasterVolume.value;
        mTextMasterVolume.text = ((int)(mMasterVolume.value * 100)).ToString();

        GameManager.instance.mBGMPlayer.volume = GameManager.instance.mSettingData.BGMVolume * GameManager.instance.mSettingData.MasterVolume;
        for (int i = 0; i < GameManager.instance.mSFXPlayer.Length; ++i)
        {
            GameManager.instance.mSFXPlayer[i].volume = GameManager.instance.mSettingData.SFXVolume * GameManager.instance.mSettingData.MasterVolume;
        }
    }
    public void SetBGMVolume()
    {
        GameManager.instance.mSettingData.BGMVolume = mBGMVolume.value;
        mTextBGMVolume.text = ((int)(mBGMVolume.value * 100)).ToString();

        GameManager.instance.mBGMPlayer.volume = GameManager.instance.mSettingData.BGMVolume * GameManager.instance.mSettingData.MasterVolume;
    }
    public void SetSFXVolume()
    {
        GameManager.instance.mSettingData.SFXVolume = mSFXVolume.value;
        mTextSFXVolume.text = ((int)(mSFXVolume.value * 100)).ToString();

        for (int i = 0; i < GameManager.instance.mSFXPlayer.Length; ++i)
        {
            GameManager.instance.mSFXPlayer[i].volume = GameManager.instance.mSettingData.SFXVolume * GameManager.instance.mSettingData.MasterVolume;
        }
    }

}
