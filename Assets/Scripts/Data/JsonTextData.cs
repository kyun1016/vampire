using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct JsonTextData
{
    public Enum.Language Type;
    public string Language;
    public string[] WeaponName;
    [TextArea]
    public string[] WeaponInitDesc;
    [TextArea]
    public string[] WeaponLvUpDesc;
    public string[] PerkName;
    [TextArea]
    public string[] PerkDesc;
    public string[] PlayerName;
    public string[] PlayerDesc;
    public string[] PlayerUnlockName;
    public string[] PlayerUnlockDesc;
    public string[] AchiveDesc;

    public string[] HUDGameStart;
    public string[] HUDLevelUpTitle;
    public string[] HUDSetting;
    public string[] HUDPowerUpTitle;
    public string[] HUDPowerUpName;
    public string[] HUDPowerUpDesc;
}
