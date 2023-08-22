using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 mInputVec;
    public float mSpeed;
    public EnemyScanner mScanner;


    Animator mAnim;
    SpriteRenderer mSpriter;
    Rigidbody2D mRigid;
    
    
    void Start()
    {
        mAnim = GetComponent<Animator>();
        mSpriter = GetComponent<SpriteRenderer>();
        mRigid = GetComponent<Rigidbody2D>();
        mScanner = GetComponent<EnemyScanner>();
    }

    void OnMove(InputValue value)
    {
        mInputVec = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 nextVec = mInputVec * mSpeed * Time.fixedDeltaTime;
        mRigid.MovePosition(mRigid.position + nextVec);
    }
    void LateUpdate()
    {
        mAnim.SetFloat("Speed", mInputVec.magnitude);
        if(mInputVec.x != 0)
        {
            mSpriter.flipX = mInputVec.x < 0;
        }
    }

    
}


// ����
//// 1. ���� �ִ� ��
//mRigid.AddForce(mInputVec);

//// 2. �ӵ� ����
//mRigid.velocity = mInputVec;

// 3. ��ġ �̵�
// Vector2 nextVec = mInputVec * mSpeed * Time.fixedDeltaTime;