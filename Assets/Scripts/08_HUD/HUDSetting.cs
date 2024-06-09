using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSetting : MonoBehaviour
{
    public TMP_Dropdown mDropdownLanguage;
    public TMP_Dropdown mDropdownWindow;
    public Toggle mToggleWindow;
    public Slider mMasterVolume;
    public Slider mBGMVolume;
    public Slider mSFXVolume;

    void InitLanguage()
    {
        for (int i=0; i<GameManager.instance.mTextJsonData.Length; ++i)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();

            option.text = GameManager.instance.mTextJsonData[i].Setting[1];
            mDropdownLanguage.options.Add(option);
        }
        mDropdownLanguage.RefreshShownValue();
    }
    void InitWindow()
    {
        for (int i = 0; i < Screen.resolutions.Length; ++i)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = Screen.resolutions[i].width + "x " + Screen.resolutions[i].height + " " + Screen.resolutions[i].refreshRate + "hz";
            mDropdownWindow.options.Add(option);
            if (Screen.resolutions[i].width == Screen.width && Screen.resolutions[i].height == Screen.height)
                mDropdownWindow.value = i;
        }
        mDropdownWindow.RefreshShownValue();
        mToggleWindow.isOn = GameManager.instance.mSettingData.Fullscreen;
    }
    void InitVolume()
    {
        mMasterVolume.value = GameManager.instance.mSettingData.MasterVolume;
        mBGMVolume.value = GameManager.instance.mSettingData.BGMVolume;
        mSFXVolume.value = GameManager.instance.mSettingData.SFXVolume;
    }
    private void Awake()
    {
        InitLanguage();
        InitWindow();
        InitVolume();
    }
}
