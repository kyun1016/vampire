using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSetting : MonoBehaviour
{
    public TMP_Dropdown mDropdownResoultion;
    public Toggle mToggleResoultion;
    public TMP_Dropdown mDropdownLanguage;

    private void Awake()
    {
        for(int i=0; i< Screen.resolutions.Length; ++i)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = Screen.resolutions[i].width + "x " + Screen.resolutions[i].height + " " + Screen.resolutions[i].refreshRate + "hz";
            mDropdownResoultion.options.Add(option);
            if (Screen.resolutions[i].width == Screen.width && Screen.resolutions[i].height == Screen.height)
                mDropdownResoultion.value = i;
        }
        mDropdownResoultion.RefreshShownValue();
        mToggleResoultion.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }
}
