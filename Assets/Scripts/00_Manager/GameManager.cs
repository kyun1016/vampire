using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool mIsLive = true;
    [Header("# Player Info")]
    public int[] mNextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Unity Data Info")]
    public SpriteAtlas mSpriteAtlas;
    public Sprite[] mSprite;
    public GameObject[] mPoolPrefabs;   // 프리펩들을 보관할 변수
    public GameObject[] mHUDPrefabs;    // 프리펩들을 보관할 변수
    public RuntimeAnimatorController[] mPlayerAnimCtrl;
    public RuntimeAnimatorController[] mEnemyAnimCtrl;
    [Header("# Json Info")]
    public PlayerJsonData[] mPlayerJsonData;
    public WeaponJsonData[] mWeaponJsonData;
    public PerkJsonData[] mPerkJsonData;
    public EnemyJsonData[] mEnemyJsonData;
    [Header("# Level & Item Info")]
    public PlayerData mPlayerData;
    public ItemCtrlData[] mPerkCtrlData;
    public ItemCtrlData[] mWeaponCtrlData;
    public PerkData mPerkData;
    public WeaponData[] mWeaponData;
    public WeaponData[] mWeaponLastData;
    [Header("# Game Object")]
    public Player mPlayer;
    public PoolManager mEnemyPool;
    public HUDLevelUp mHUDLevelUp;

    void Awake()
    {
        instance = this;
        LoadFromJson();

        mEnemyPool = new GameObject().AddComponent<PoolManager>();
        mEnemyPool.transform.name = "EnemyPool";
        mEnemyPool.transform.parent = GameManager.instance.transform;
        mEnemyPool.Init(mEnemyJsonData[0].PrefabId);

        mPerkCtrlData = new ItemCtrlData[mPlayerJsonData[0].MaxPerkSize];
        mWeaponCtrlData = new ItemCtrlData[mPlayerJsonData[0].MaxWeaponSize];
        mWeaponData = new WeaponData[mPlayerJsonData[0].MaxWeaponSize];
        mWeaponLastData = new WeaponData[mPlayerJsonData[0].MaxWeaponSize];

        mPlayerData.Id = 0;
        mPlayerData.WeaponSize = 0;
        mPlayerData.PerkSize = 0;

        FuncWeapon.ClearPerk();
        FuncWeapon.UpdatePlayerMovement();
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
        mPlayerData.Health = mPlayerJsonData[mPlayerData.Id].MaxHealth;

        mHUDLevelUp.Select(5);
    }

    void Update()
    {
        if (!mIsLive)
            return;

        mPlayerData.GameTime += Time.deltaTime;

        if (mPlayerData.GameTime > mPlayerJsonData[mPlayerData.Id].MaxGameTime)
        {
            mPlayerData.GameTime = mPlayerJsonData[mPlayerData.Id].MaxGameTime;
        }
    }

    public void GetExp(int exp)
    {
        mPlayerData.Exp += exp;

        if (mPlayerData.Exp >= mNextExp[Mathf.Min(mPlayerData.Level, mNextExp.Length - 1)])
        {
            mPlayerData.Exp -= mNextExp[Mathf.Min(mPlayerData.Level, mNextExp.Length - 1)];
            mPlayerData.Level++;
            mPlayerData.Health = mPlayerJsonData[mPlayerData.Id].MaxHealth;
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
