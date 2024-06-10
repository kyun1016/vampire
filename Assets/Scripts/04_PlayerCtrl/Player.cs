using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 mInputVec;
    public Vector2 mLastDir;
    public EnemyScanner mScanner;
    public WeaponCtrl[] mWeaponCtrl;
    float mTimerEnemyLevel;
    float mTimerEnemySpawn;
    int mEnemyLevel;

    Animator mAnim;
    SpriteRenderer mSpriter;
    Rigidbody2D mRigid;

    private void Awake()
    {
        mAnim = GetComponent<Animator>();
        mSpriter = GetComponent<SpriteRenderer>();
        mRigid = GetComponent<Rigidbody2D>();
        mScanner = GetComponent<EnemyScanner>();
    }

    private void OnEnable()
    {
        mAnim.runtimeAnimatorController = GameManager.instance.mPlayerAnimCtrl[GameManager.instance.mPlayerData.AnimCtrlId];
        mWeaponCtrl = new WeaponCtrl[GameManager.instance.mWeaponCtrlData.Length];
        mTimerEnemySpawn = 0;
        mTimerEnemyLevel = 0;

        for (int i = 0; i < mWeaponCtrl.Length; ++i)
        {
            mWeaponCtrl[i] = new GameObject("WeaponCtrl" + i).AddComponent<WeaponCtrl>();
            mWeaponCtrl[i].transform.parent = GameManager.instance.mPlayer.transform;
            mWeaponCtrl[i].transform.localPosition = Vector3.zero;
            mWeaponCtrl[i].transform.localRotation = Quaternion.identity;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = GameManager.instance.mEnemyPool.Get();
        enemy.transform.position = GameManager.instance.mPlayer.transform.position;
        Vector3 pos = Random.insideUnitCircle;
        if (pos.magnitude == 0)
            pos.x = 1;
        pos = pos.normalized;
        enemy.transform.position += pos * 20;
        enemy.GetComponent<Enemy>().Init(mEnemyLevel);
    }

    void SpawnObject()
    {
        GameObject enemy = GameManager.instance.mDropPool.Get();
        enemy.transform.position = GameManager.instance.mPlayer.transform.position;
        Vector3 pos = Random.insideUnitCircle;
        if (pos.magnitude == 0)
            pos.x = 1;
        pos = pos.normalized;
        enemy.transform.position += pos * 20;
        enemy.GetComponent<Enemy>().Init(mEnemyLevel);
    }

    void OnMove(InputValue value)
    {
        if (!GameManager.instance.mIsLive)
            return;
        mInputVec = value.Get<Vector2>();
        if (mInputVec.magnitude != 0)
            mLastDir = mInputVec.normalized;
    }
    void FixedUpdate()
    {
        if (!GameManager.instance.mIsLive)
            return;
        // 이동
        Vector2 nextVec = mInputVec * GameManager.instance.mPlayerData.MovementSpeed * Time.fixedDeltaTime;
        mRigid.MovePosition(mRigid.position + nextVec);

        // 적 생성
        mTimerEnemySpawn += Time.fixedDeltaTime;
        if (mTimerEnemySpawn > GameManager.instance.mEnemyJsonData[mEnemyLevel].SpawnTime)
        {
            mTimerEnemySpawn = 0;
            for (int i = 0; i < GameManager.instance.mEnemyJsonData[mEnemyLevel].SpawnCount; ++i)
                SpawnEnemy();
        }

        // 적 레벨업
        mTimerEnemyLevel += Time.fixedDeltaTime;
        if (mTimerEnemyLevel > GameManager.instance.mEnemyJsonData[mEnemyLevel].LevelUpTime && mEnemyLevel < GameManager.instance.mEnemyJsonData.Length - 1)
        {
            mTimerEnemyLevel = 0;
            mTimerEnemySpawn = 0;
            ++mEnemyLevel;
        }
    }
    void LateUpdate()
    {
        if (!GameManager.instance.mIsLive)
            return;
        mAnim.SetFloat("Speed", mInputVec.magnitude);
        if(mInputVec.x != 0)
        {
            mSpriter.flipX = mInputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.mIsLive)
            return;

        GameManager.instance.mPlayerData.Health -= 10 * Time.deltaTime;

        if (GameManager.instance.mPlayerData.Health < 0)
        {
            for (int i = 1; i < GameManager.instance.mPlayer.transform.childCount; ++i)
                GameManager.instance.mPlayer.transform.GetChild(i).gameObject.SetActive(false);

            mAnim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}