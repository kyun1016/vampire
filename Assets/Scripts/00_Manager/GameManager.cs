using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float mGameTime;
    public float mMaxGameTime = 5 * 10f;

    public Player mPlayer;
    public PoolManager mPoolManager;
    
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        mGameTime += Time.deltaTime;

        if (mGameTime > mMaxGameTime)
        {
            mGameTime = mMaxGameTime;
        }
    }
}
