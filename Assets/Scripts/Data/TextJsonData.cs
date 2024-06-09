using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TextJsonData
{
    public Enum.Language Type;
    public string[] WeaponName;
    [TextArea]
    public string[] WeaponInitDesc;
    public string[] WeaponLvUpDesc;
    public string[] PlayerName;
    public string[] PlayerDesc;
    public string[] PlayerUnlockDesc;

    public string[] AchiveDesc;

    public string[] Setting;
}
