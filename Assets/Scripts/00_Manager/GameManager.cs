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
    [Header("# Unity Data Info")]
    public Sprite[] mSprite;
    public GameObject[] mPrefabs;               // ��������� ������ ����
    public RuntimeAnimatorController[] mPlayerAnimCtrl;
    public RuntimeAnimatorController[] mEnemyAnimCtrl;
    [Header("# Json Info")]
    public int mPlayerJsonId;
    public PlayerJsonData[] mPlayerJsonData;
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
    public Player mPlayer;
    public PoolManager mEnemyPool;
    public LevelUp mHUDLevelUp;

    void Awake()
    {
        instance = this;

        mEnemyPool = new GameObject().AddComponent<PoolManager>();
        mEnemyPool.transform.name = "EnemyPool";
        mEnemyPool.transform.parent = GameManager.instance.transform;
        mEnemyPool.Init(mEnemyJsonData[0].PrefabId);

        mWeaponCtrlData = new ItemCtrlData[2];
        mPerkCtrlData = new ItemCtrlData[2];
        mWeaponData = new WeaponData[2];
        mWeaponLastData = new WeaponData[2];

        FuncWeapon.ClearPerk();
    }

    public void SaveToJson()
    {
        string json = JsonConvert.SerializeObject(mEnemyJsonData);
        File.WriteAllText(Application.dataPath + "/Datas/EnemyJsonData.json", json);
        json = JsonConvert.SerializeObject(mWeaponJsonData);
        File.WriteAllText(Application.dataPath + "/Datas/WeaponJsonData.json", json);
        json = JsonConvert.SerializeObject(mPerkJsonData);
        File.WriteAllText(Application.dataPath + "/Datas/PerkJsonData.json", json);
        json = JsonConvert.SerializeObject(mPlayerJsonData);
        File.WriteAllText(Application.dataPath + "/Datas/PlayerJsonData.json", json);
    }
    private void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/Datas/EnemyJsonData.json");
        mEnemyJsonData = JsonConvert.DeserializeObject<EnemyJsonData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/PlayerJsonData.json");
        mPlayerJsonData = JsonConvert.DeserializeObject<PlayerJsonData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/WeaponJsonData.json");
        mWeaponJsonData = JsonConvert.DeserializeObject<WeaponJsonData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/PerkJsonData.json");
        mPerkJsonData = JsonConvert.DeserializeObject<PerkJsonData[]>(json);
    }

    private void Start()
    {
        LoadFromJson();
        
        mPlayerData.Health = mPlayerJsonData[mPlayerJsonId].MaxHealth;

        mHUDLevelUp.Select(1);
        mHUDLevelUp.Select(1);
        mHUDLevelUp.Select(1);
    }

    void Update()
    {
        if (!mIsLive)
            return;

        mPlayerData.GameTime += Time.deltaTime;

        if (mPlayerData.GameTime > mPlayerJsonData[mPlayerJsonId].MaxGameTime)
        {
            mPlayerData.GameTime = mPlayerJsonData[mPlayerJsonId].MaxGameTime;
        }
    }

    public void GetExp(int exp)
    {
        mPlayerData.Exp += exp;

        if (mPlayerData.Exp >= mNextExp[Mathf.Min(mPlayerData.Level, mNextExp.Length - 1)])
        {
            mPlayerData.Exp -= mNextExp[Mathf.Min(mPlayerData.Level, mNextExp.Length - 1)];
            mPlayerData.Level++;
            mPlayerData.Health = mPlayerJsonData[mPlayerJsonId].MaxHealth;
            mHUDLevelUp.Show();
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
