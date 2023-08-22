using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mDamege;
    public int mPer;

    Rigidbody2D mRigid;

    private void Awake()
    {
        mRigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage, int per, Vector3 dir)
    {
        mDamege = damage;
        mPer = per;

        if(per > -1)
        {
            mRigid.velocity = dir;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") | mPer == -1)
            return;

        mPer--;
        if(mPer == -1)
        {
            mRigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
