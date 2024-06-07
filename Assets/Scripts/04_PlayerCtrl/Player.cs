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

    Animator mAnim;
    SpriteRenderer mSpriter;
    Rigidbody2D mRigid;
    public void init()
    {
        mAnim.runtimeAnimatorController = GameManager.instance.mPlayerAnimCtrl[GameManager.instance.mPlayerData.SpriteId];
        mWeaponCtrl = new WeaponCtrl[GameManager.instance.mWeaponCtrlData.Length];

        for (int i = 0; i < mWeaponCtrl.Length; ++i)
        {
            mWeaponCtrl[i] = new GameObject().AddComponent<WeaponCtrl>();
            mWeaponCtrl[i].transform.name = "WeaponCtrl" + i;
            mWeaponCtrl[i].transform.parent = GameManager.instance.mPlayer.transform;
            mWeaponCtrl[i].transform.localPosition = Vector3.zero;
            mWeaponCtrl[i].transform.localRotation = Quaternion.identity;
        }
    }
    void Start()
    {
        mAnim = GetComponent<Animator>();
        mSpriter = GetComponent<SpriteRenderer>();
        mRigid = GetComponent<Rigidbody2D>();
        mScanner = GetComponent<EnemyScanner>();
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
        Vector2 nextVec = mInputVec * GameManager.instance.mPlayerData.MovementSpeed * Time.fixedDeltaTime;
        mRigid.MovePosition(mRigid.position + nextVec);
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