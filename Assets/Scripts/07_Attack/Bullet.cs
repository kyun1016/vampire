using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mDamege;
    public int mPer;
    Vector3 mDir;
    public float mSpeed;
    bool mIsLive;

    private void FixedUpdate()
    {
        if (!mIsLive)
            return;

        transform.position += mDir * mSpeed * Time.deltaTime;
        
        if (Vector3.Distance(transform.position, transform.parent.transform.position) > 20)
        {
            mIsLive = false;
            gameObject.SetActive(false);
        }
    }
    public void Init(float damage, int per, Vector3 dir, float speed)
    {
        mIsLive = true;
        mDamege = damage;
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
