using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemData
{
    public int Id;
    public eItemType ItemType;
    public string Name;
    [TextArea]
    public string Desc;
    public int[] Counts;
    public float[] Damages;
    public float[] Speeds;
    public float[] CoolTimes;   // Range Weapon

    public GameObject Projectile;
}
