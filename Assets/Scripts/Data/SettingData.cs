using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SettingData
{
    public Enum.Language LanguageType;
    public float MasterVolume;
    public float BGMVolume;
    public float SFXVolume;
    public int ResolutionNum;
    public int Width;
    public int Height;
    public bool Fullscreen;
    public int RefreshRate;
}
