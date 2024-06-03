using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 mInputVec;
    public float mSpeed;
    public EnemyScanner mScanner;

    public WeaponCtrl[] mWeaponCtrl;

    Animator mAnim;
    SpriteRenderer mSpriter;
    Rigidbody2D mRigid;
    void Awake()
    {
        mWeaponCtrl = new WeaponCtrl[8];

        for (int i = 0; i < mWeaponCtrl.Length; ++i)
        {
            mWeaponCtrl[i] = new GameObject().AddComponent<WeaponCtrl>();
            mWeaponCtrl[i].transform.name = "WeaponCtrl" + i;
            mWeaponCtrl[i].transform.parent = GameManager.instance.mPlayer.transform;
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
    }
    void FixedUpdate()
    {
        if (!GameManager.instance.mIsLive)
            return;
        Vector2 nextVec = mInputVec * mSpeed * Time.fixedDeltaTime;
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
}