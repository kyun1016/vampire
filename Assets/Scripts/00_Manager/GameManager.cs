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
    [Header("# Player Info")]
    public int[] mNextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Item Info")]
    public Sprite[] mItemSprite;
    [Header("# Json Info")]
    public WeaponJsonData[] mWeaponJsonData;
    public PerkJsonData[] mPerkJsonData;
    public EnemyJsonData[] mEnemyJsonData;
    [Header("# Level & Item Info")]
    public PlayerData mPlayerData;
    public int mWeaponSize;
    public int mPerkSize;
    public ItemCtrlData[] mWeaponCtrlData;
    public ItemCtrlData[] mPerkCtrlData;
    public PerkData mPerkData;
    public WeaponData[] mWeaponData;
    public WeaponData[] mWeaponLastData;
    [Header("# Game Object")]
    public GameObject[] mPrefabs;               // 프리펩들을 보관할 변수
    public RuntimeAnimatorController[] mPlayerAnimCtrl;
    public RuntimeAnimatorController[] mEnemyAnimCtrl;
    public Player mPlayer;
    public PoolManager mEnemyPool;
    public LevelUp mUILevelUp;

    void Awake()
    {
        instance = this;

        mEnemyPool = new GameObject().AddComponent<PoolManager>();
        mEnemyPool.transform.name = "EnemyPool";
        mEnemyPool.transform.parent = GameManager.instance.transform;
        mEnemyPool.Init(mEnemyJsonData[0].PrefabId);
    }

    public void SaveToJson()
    {
        string json = JsonConvert.SerializeObject(mEnemyJsonData);
        File.WriteAllText(Application.dataPath + "/Datas/EnemyJsonData.json", json);
        json = JsonConvert.SerializeObject(mWeaponJsonData);
        File.WriteAllText(Application.dataPath + "/Datas/WeaponJsonData.json", json);
        json = JsonConvert.SerializeObject(mPerkJsonData);
        File.WriteAllText(Application.dataPath + "/Datas/PerkJsonData.json", json);
    }
    private void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/Datas/EnemyData.json");
        mEnemyJsonData = JsonConvert.DeserializeObject<EnemyJsonData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/WeaponData.json");
        mWeaponJsonData = JsonConvert.DeserializeObject<WeaponJsonData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/PerkData.json");
        mPerkJsonData = JsonConvert.DeserializeObject<PerkJsonData[]>(json);
    }

    private void Start()
    {
        LoadFromJson();
        
        mPlayerData.Health = mPlayerData.MaxHealth;

        mUILevelUp.Select(0);
    }

    void Update()
    {
        if (!mIsLive)
            return;

        mPlayerData.GameTime += Time.deltaTime;

        if (mPlayerData.GameTime > mPlayerData.MaxGameTime)
        {
            mPlayerData.GameTime = mPlayerData.MaxGameTime;
        }
    }

    public void GetExp(int exp)
    {
        mPlayerData.Exp += exp;

        if (mPlayerData.Exp >= mNextExp[Mathf.Min(mPlayerData.Level, mNextExp.Length - 1)])
        {
            mPlayerData.Exp -= mNextExp[Mathf.Min(mPlayerData.Level, mNextExp.Length - 1)];
            mPlayerData.Level++;
            mPlayerData.Health = mPlayerData.MaxHealth;
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
