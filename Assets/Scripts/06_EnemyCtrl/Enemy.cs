using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float mSpeed;
    public float mHealth;
    public float mMaxHealth;
    public RuntimeAnimatorController[] mAnimCtrl;
    public Rigidbody2D mTarget;

    bool mIsLive;

    Rigidbody2D mRigid;
    Animator mAnim;
    SpriteRenderer mSpriter;

    private void Awake()
    {
        mRigid = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
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

    private void OnEnable()
    {
        mTarget = GameManager.instance.mPlayer.GetComponent<Rigidbody2D>();
        mIsLive = true;
        mHealth = mMaxHealth;
    }

    public void Init(DataSpawn data)
    {
        mAnim.runtimeAnimatorController = mAnimCtrl[data.spriteType];
        mSpeed = data.speed;
        mMaxHealth = data.health;
        mHealth = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.CompareTag("Bullet") || collision.CompareTag("Melee")))
            return;
        if (collision.CompareTag("Bullet"))
            mHealth -= collision.GetComponent<Bullet>().mDamege;
        else if (collision.CompareTag("Melee"))
            mHealth -= collision.GetComponent<Melee>().mDamege;
        if (mHealth > 0)
        {

        }
        else
        {
            Dead();
        }
    }
    
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
