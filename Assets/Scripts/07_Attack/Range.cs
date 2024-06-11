using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Weapon
{
    bool mIsLive;
    int mPer;
    float mSpeed;
    Vector3 mDir;
    
    private void FixedUpdate()
    {
        if (!mIsLive)
            return;
        transform.position += mDir * mSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, GameManager.instance.mPlayer.transform.position) > 20)
        {
            mIsLive = false;
            gameObject.SetActive(false);
        }
    }
    public void Init(float damage, int per, Vector3 dir, float speed, float debuffPower = 0, Enum.DebuffType debuff = Enum.DebuffType.None, Enum.EffectType effect = Enum.EffectType.None)
    {
        mIsLive = true;
        mDebuff = debuff;
        mEffect = effect;

        mDamage = damage;
        mPer = per;
        mDir = dir;
        mSpeed = speed;
        mDebuffPower = debuffPower;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        mPer--;

        if (mEffect == Enum.EffectType.Stop)
        {
            transform.localScale = new Vector3(1.0f, 1.0f);
            mSpeed = 0;
        }


        if (mPer == -1)
        {
            mIsLive = false;
            gameObject.SetActive(false);
        }
    }
}
