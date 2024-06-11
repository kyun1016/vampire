using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Enum.DebuffType mDebuff;
    public Enum.EffectType mEffect;
    public float mDamage;
    public float mDebuffPower;
    public float mSpeed;

    public void Init(float damage, float debuffPower = 0, Enum.DebuffType debuff = Enum.DebuffType.None, Enum.EffectType effect = Enum.EffectType.None)
    {
        mDebuff = debuff;
        mEffect = effect;
        mDamage = damage;
        mSpeed = 0;
        mDebuffPower = debuffPower;
    }
}
