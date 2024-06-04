using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("# Enemy Data")]
    bool mIsLive;
    public float mMovementSpeed;
    public float mHealth;
    public float mMaxHealth;
    public float mKnockBackForce;
    public int mDropExp;
    
    [Header("# Game Object")]
    public Rigidbody2D mTarget;

    Rigidbody2D mRigid;
    Collider2D mColl;
    Animator mAnim;
    SpriteRenderer mSpriter;
    WaitForFixedUpdate mWaitFixedFrame;

    private void Awake()
    {
        mRigid = GetComponent<Rigidbody2D>();
        mColl = GetComponent<Collider2D>();
        mAnim = GetComponent<Animator>();
        mSpriter = GetComponent<SpriteRenderer>();
        mWaitFixedFrame = new WaitForFixedUpdate(); 
    }
    public void Init(int id)
    {
        EnemyJsonData data = GameManager.instance.mEnemyJsonData[id];
        mAnim.runtimeAnimatorController = GameManager.instance.mEnemyAnimCtrl[data.AnimCtrlId];
        mMovementSpeed = data.MovementSpeed;
        mMaxHealth = data.MaxHealth;
        mHealth = data.MaxHealth;
        mKnockBackForce = data.KnockBackForce;
        mDropExp = data.DropExp;
    }
    private void OnEnable()
    {
        mTarget = GameManager.instance.mPlayer.GetComponent<Rigidbody2D>();
        mIsLive = true;
        mColl.enabled = true;
        mRigid.simulated = true;
        mSpriter.sortingOrder = 2;
        mAnim.SetBool("Dead", false);
        mHealth = mMaxHealth;
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.mIsLive)
            return;
        if (!mIsLive || mAnim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = mTarget.position - mRigid.position;
        Vector2 nextVec = dirVec.normalized * mMovementSpeed * Time.fixedDeltaTime;
        mRigid.MovePosition(mRigid.position + nextVec);

        mRigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.mIsLive)
            return;
        if (!mIsLive)
            return;

        mSpriter.flipX = mTarget.position.x < mRigid.position.x;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.CompareTag("Bullet") || collision.CompareTag("Melee")) || !mIsLive)
            return;
        if (collision.CompareTag("Bullet"))
            mHealth -= collision.GetComponent<Range>().mDamage;
        else if (collision.CompareTag("Melee"))
            mHealth -= collision.GetComponent<Melee>().mDamage;

        if (mHealth > 0)
        {
            StartCoroutine(KnockBack());
            mAnim.SetTrigger("Hit");
        }
        else
        {
            mIsLive = false;
            mColl.enabled = false;
            mRigid.simulated = false;
            mSpriter.sortingOrder = 1;
            mAnim.SetBool("Dead", true);
            GameManager.instance.mPlayerData.Kill++;
            GameManager.instance.GetExp(mDropExp);
        }
    }

    IEnumerator KnockBack()
    {
        //yield return null; // 1 프레임 쉬기
        //yield return new WaitForSeconds(2f); // 2초 쉬기
        yield return mWaitFixedFrame;

        Vector3 playerPos = GameManager.instance.mPlayer.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        mRigid.AddForce(dirVec.normalized * mKnockBackForce, ForceMode2D.Impulse);
    }
    
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
