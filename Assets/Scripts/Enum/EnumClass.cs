using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enum
{
    public enum InfoType { 
        Exp, 
        Level, 
        Kill, 
        Time, 
        Health 
    }
    public enum WeaponType
    {
        Melee,
        RangeBullet,
        RangeDagger,
        RangeAxe,
        Garlic,
        Web,
        Boom,
        ThrowBoom
    }
    public enum DescType
    {
        Melee,
        Range,
        Perk,
        Heal
    }
}
