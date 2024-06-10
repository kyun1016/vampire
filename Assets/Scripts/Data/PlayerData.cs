using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    public int Id;
    public int AnimCtrlId;
    public float GameTime;
    public float MovementSpeed;
    public float MaxHealth;
    public float Health;
    public int Level;
    public int Kill;
    public int Exp;
    public int Gold;
    public int WeaponSize;
    public int PerkSize;
}
