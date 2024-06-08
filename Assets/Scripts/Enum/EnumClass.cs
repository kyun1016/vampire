using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enum
{
    public enum Achive
    {
        UnlockChar2,
        UnlockChar3,
    }
    public enum PrefabType
    {
        Enemy,
        MeleeBox,
        MeleeCircle,
        RangeBox,
        RangeCircle
    }
    public enum WeaponTag
    {
        Bullet,
        Melee,
        SpiderWeb,
        Garlic
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
