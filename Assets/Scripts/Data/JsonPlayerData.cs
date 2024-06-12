using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct JsonPlayerData
{
    public bool Enable;
    public int AnimCtrlId;
    public int StartWeaponId;
    public float MaxGameTime;
    public float MaxHealth;
    public int MovementSpeed;
    public int MaxWeaponSize;
    public int MaxPerkSize;
}
