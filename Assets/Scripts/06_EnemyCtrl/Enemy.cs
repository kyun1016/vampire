using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("# Enemy Data")]
    bool mIsLive;
    public float[] mDebuffPowers;
    public float mMovementSpeed;
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
        mDebuffPowers = new float[(int)Enum.DebuffType.Size];
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
        for (int i = 0; i < mDebuffPowers.Length; ++i)
        {
            mDebuffPowers[i] = 0;
        }
        mDebuffPowers[(int)Enum.DebuffType.SlowCoef] = 1.0f;    // 슬로우의 경우 coef로 활용

        mColl.enabled = true;
        mRigid.simulated = true;
        mSpriter.sortingOrder = 2;
        mAnim.SetBool("Dead", false);
        mHealth = mMaxHealth;
    }

    private void Dead()
    {
        mIsLive = false;
        mColl.enabled = false;
        mRigid.simulated = false;
        mSpriter.sortingOrder = 1;
        mAnim.SetBool("Dead", true);
        GameManager.instance.mPlayerData.Kill++;

        // 경험치 드롭
        FuncPool.DropExp(mDropExp, transform.position);

        // 골드 드롭
        if (Random.value <= mDropGoldChance)
        {
            FuncPool.DropGold(mDropGold, transform.position);
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

        if (mDebuffPowers[(int)Enum.DebuffType.TicDamage] > 0)
            Hit(mDebuffPowers[(int)Enum.DebuffType.TicDamage] * Time.fixedDeltaTime);

        Vector2 dirVec = mTarget.position - mRigid.position;
        Vector2 nextVec = dirVec.normalized * mMovementSpeed * mDebuffPowers[(int)Enum.DebuffType.SlowCoef] * Time.fixedDeltaTime;
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
        if (!(collision.CompareTag("Weapon")) || !mIsLive)
            return;

        Hit(collision.GetComponent<Weapon>().mDamage);
        mDebuffPowers[(int)collision.GetComponent<Weapon>().mDebuff] += collision.GetComponent<Weapon>().mDebuffPower;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!(collision.CompareTag("Weapon")) || !mIsLive)
            return;

        mDebuffPowers[(int)collision.GetComponent<Weapon>().mDebuff] -= collision.GetComponent<Weapon>().mDebuffPower;
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
