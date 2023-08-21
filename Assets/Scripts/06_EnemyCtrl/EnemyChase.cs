using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float mSpeed;
    public Rigidbody2D mTarget;

    bool mIsLive;

    Rigidbody2D mRigid;
    SpriteRenderer mSpriter;

    private void Awake()
    {
        mRigid = GetComponent<Rigidbody2D>();
        mSpriter = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (!mIsLive)
            return;

        Vector2 dirVec = mTarget.position - mRigid.position;
        Vector2 nextVec = dirVec.normalized * mSpeed * Time.fixedDeltaTime;
        mRigid.MovePosition(mRigid.position + nextVec);

        mRigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!mIsLive)
            return;

        mSpriter.flipX = mTarget.position.x < mRigid.position.x;
    }
}
