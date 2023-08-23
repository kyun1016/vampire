using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float mGameTime;
    public float mMaxGameTime;
    [Header("# Player Info")]
    public int mLevel;
    public int mKill;
    public int mExp;
    public int[] mNextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
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

    public void GetExp(int exp)
    {
        mExp += exp;

        if (mExp >= mNextExp[mLevel])
        {
            mExp -= mNextExp[mLevel];
            mLevel++;
        }
    }
}
