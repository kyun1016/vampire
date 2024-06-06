using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    bool mIsLive;
    public float mDamage;
    int mPer;
    public Vector3 mDir;
    float mSpeed;

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
    public void Init(float damage, int per, Vector3 dir, float speed)
    {
        mIsLive = true;
        mDamage = damage;
        mPer = per;
        mDir = dir;
        mSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        mPer--;
        if(mPer == -1)
        {
            mIsLive = false;
            gameObject.SetActive(false);
        }
    }
}
