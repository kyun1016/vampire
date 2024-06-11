using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("# Enemy Data")]
    bool mIsLive;
    public bool mIsGarlic;
    public float mGarlicDamage;
    public float mMovementSpeed;
    public float mMovementSpeedCoef;
    public float mHealth;
    public float mMaxHealth;
    public float mKnockBackForce;
    public int mDropExp;
    public int mDropGold;
    public float mDropGoldChance;
    
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
        mDropGold = data.DropGold;
        mDropGoldChance = data.DropGoldChance;
    }



    private void OnEnable()
    {
        mTarget = GameManager.instance.mPlayer.GetComponent<Rigidbody2D>();
        mIsLive = true;
        mIsGarlic = false;
        mMovementSpeedCoef = 1f;
        mColl.enabled = true;
        mRigid.simulated = true;
        mSpriter.sortingOrder = 2;
        mAnim.SetBool("Dead", false);
        mHealth = mMaxHealth;
    }

    private void Dead()
    {
        mIsLive = false;
        mIsGarlic = false;
        mMovementSpeedCoef = 1.0f;
        mColl.enabled = false;
        mRigid.simulated = false;
        mSpriter.sortingOrder = 1;
        mAnim.SetBool("Dead", true);
        GameManager.instance.mPlayerData.Kill++;

        // 경험치 드롭
        FuncPool.DropExp(mDropExp, transform);

        // 골드 드롭
        if (Random.value <= mDropGoldChance)
        {
            FuncPool.DropGold(mDropGold, transform);
        }

        GameManager.instance.PlaySFX(Enum.SFX.Dead);

        gameObject.SetActive(false);
    }

    void Hit(float damage)
    {
        mHealth -= damage;
        GameManager.instance.PlaySFX(Enum.SFX.Hit0);

        if (mHealth > 0)
        {
            StartCoroutine(KnockBack());
            mAnim.SetTrigger("Hit");
        }
        else
        {
            Dead();
        }
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.mIsLive)
            return;
        if (!mIsLive || mAnim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        if(mIsGarlic)
        {
            Hit(mGarlicDamage);
        }

        Vector2 dirVec = mTarget.position - mRigid.position;
        Vector2 nextVec = dirVec.normalized * mMovementSpeed * mMovementSpeedCoef * Time.fixedDeltaTime;
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
        if (!(collision.CompareTag("Melee") || collision.CompareTag("Range")) || !mIsLive)
            return;
        //if (collision.CompareTag("Garlic"))
        //{
        //    mIsGarlic = true;
        //    mGarlicDamage = collision.GetComponent<Melee>().mDamage;
        //    return;
        //}
        //if (collision.CompareTag("SpiderWeb"))
        //{
        //    mMovementSpeedCoef *= 0.5f;
        //    collision.GetComponent<Range>().transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //    collision.GetComponent<Range>().mSpeed = 0f;
        //    Hit(collision.GetComponent<Range>().mDamage);
        //}

        if (collision.CompareTag("Range"))
            Hit(collision.GetComponent<Range>().mDamage);
        else if (collision.CompareTag("Melee"))
            Hit(collision.GetComponent<Melee>().mDamage);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.CompareTag("Garlic"))
        //    mIsGarlic = false;
        //if (collision.CompareTag("SpiderWeb"))
        //    mMovementSpeedCoef *= 2f;
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


}
