using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool mIsLive = true;
    public float mGameTime;
    public float mMaxGameTime;
    [Header("# Player Info")]
    public int mHealth;
    public int mMaxHealth = 100;
    public int mLevel;
    public int mKill;
    public int mExp;
    public int[] mNextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    public int[] mWeaponId = { 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] mWeaponLevel = { 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] mGearId = { 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] mGearLevel = { 0, 0, 0, 0, 0, 0, 0, 0 };
    [Header("# Item Info")]
    public Sprite[] mItemSprite;
    public GameObject[] mItemProjectile;
    public int[] mItemLevel;
    [Header("# Json Info")]
    public ItemData[] mItemData;
    public EnemyData[] mEnemyData;
    [Header("# Game Object")]
    public GameObject[] mPrefabs;               // 프리펩들을 보관할 변수
    public RuntimeAnimatorController[] mPlayerAnimCtrl;
    public RuntimeAnimatorController[] mEnemyAnimCtrl;

    public Player mPlayer;
    public PoolManager mPoolManager;
    public LevelUp mUILevelUp;

    void Awake()
    {
        instance = this;
        GameObject newPoolManager = new GameObject();
        mPoolManager = newPoolManager.AddComponent<PoolManager>();
        mPoolManager.transform.name = "EnemyPool";
        mPoolManager.transform.parent = GameManager.instance.transform;

        GameObject newPlayer = new GameObject();
        // mPlayer = newPlayer.AddComponent<Player>();
    }

    public void SaveToJson()
    {
        string json = JsonConvert.SerializeObject(mItemData);

        File.WriteAllText(Application.dataPath + "/Datas/ItemData.json", json);
    }
    private void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/Datas/EnemyData.json");
        mEnemyData = JsonConvert.DeserializeObject<EnemyData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/ItemData.json");
        mItemData = JsonConvert.DeserializeObject<ItemData[]>(json);
    }

    private void Start()
    {
        LoadFromJson();
        
        mHealth = mMaxHealth;

        mUILevelUp.Select(0);
    }

    void Update()
    {
        if (!mIsLive)
            return;

        mGameTime += Time.deltaTime;

        if (mGameTime > mMaxGameTime)
        {
            mGameTime = mMaxGameTime;
        }
    }

    public void GetExp(int exp)
    {
        mExp += exp;

        if (mExp >= mNextExp[Mathf.Min(mLevel, mNextExp.Length - 1)])
        {
            mExp -= mNextExp[Mathf.Min(mLevel, mNextExp.Length - 1)];
            mLevel++;
            mHealth = mMaxHealth;
            mUILevelUp.Show();
        }
    }

    public void Stop()
    {
        mIsLive = false;

        Time.timeScale = 0;
    }
    public void Resume()
    {
        mIsLive = true;

        Time.timeScale = 1;
    }
}
