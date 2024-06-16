using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool mIsLive = true;
    [Header("# Player Info")]
    public int[] mNextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Unity Data Info")]
    public SpriteAtlas mSpriteAtlas;
    public Sprite[] mPlayerSprite;
    public Sprite[] mItemSprite;
    public Sprite[] mAchiveSprite;
    public GameObject[] mPoolPrefabs;   // 프리펩들을 보관할 변수
    public RuntimeAnimatorController[] mPlayerAnimCtrl;
    public RuntimeAnimatorController[] mEnemyAnimCtrl;
    [Header("# Json Info")]
    public AchiveJsonData[] mAchiveJsonData;
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
    public HUDGameStart mHUDGameStart;
    public HUDAchive mHUDAchive;
    public HUDInGame mHUDInGame;
    public HUDLevelUp mHUDLevelUp;
    public HUDResult mHUDResult;
    public WaitForSeconds mWait0_5s;
    public WaitForSecondsRealtime mWait5s;

    // Part 1. Achive
    public void InitAchive()
    {
        PlayerPrefs.SetInt("MyData", 1);

        for (int i = 0; i < mAchiveJsonData.Length; ++i)
        {
            PlayerPrefs.SetInt(mAchiveJsonData[i].Name.ToString(), 0);
        }
    }
    void CheckAchive()
    {
        for (int i= 0; i< mAchiveJsonData.Length; ++i)
        {
            bool isAchive = false;
            switch (mAchiveJsonData[i].Name)
            {
                case Enum.Achive.UnlockChar2:
                    if (mPlayerData.Kill >= 1) isAchive = true;
                    break;
                case Enum.Achive.UnlockChar3:
                    if (mPlayerData.GameTime >= 300) isAchive = true;
                    break;
                default:
                    Debug.Assert(false, "Error");
                    break;
            }
            if (isAchive && PlayerPrefs.GetInt(mAchiveJsonData[i].Name.ToString()) == 0)
            {
                PlayerPrefs.SetInt(mAchiveJsonData[i].Name.ToString(), 1);
                StartCoroutine(NoticeRoutine(i));
            }
        }
    }
    // Part 2. Json
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
        json = JsonConvert.SerializeObject(mAchiveJsonData);
        File.WriteAllText(Application.dataPath + "/Datas/AchiveJsonData.json", json);
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

        json = File.ReadAllText(Application.dataPath + "/Datas/AchiveJsonData.json");
        mAchiveJsonData = JsonConvert.DeserializeObject<AchiveJsonData[]>(json);
    }

    // Part 3. Awake
    void Awake()
    {
        
        instance = this;
        mIsLive = false;
        // Part 1. Json Load
        LoadFromJson();
        // Part 2. Make Dynamic Array
        mWait0_5s = new WaitForSeconds(0.5f);
        mWait5s = new WaitForSecondsRealtime(5);
        mEnemyPool = new GameObject().AddComponent<PoolManager>();
        // Part 3. Init Enemy Pool
        mEnemyPool.transform.name = "EnemyPool";
        mEnemyPool.transform.parent = GameManager.instance.transform;
        mEnemyPool.Init(mEnemyJsonData[0].PrefabId);
        // Part 4. Init Player Data
        FuncWeapon.ClearPerk();
        FuncWeapon.UpdatePlayerMovement();
        // Part 5. Init Achive List
        if (!PlayerPrefs.HasKey("MyData"))
        {
            InitAchive();
        }
    }
    // Part 4. Update
    void Update()
    {
        if (!mIsLive)
            return;

        mPlayerData.GameTime += Time.deltaTime;
        CheckAchive();

        if (mPlayerData.GameTime > mPlayerJsonData[mPlayerData.Id].MaxGameTime)
        {
            mPlayerData.GameTime = mPlayerJsonData[mPlayerData.Id].MaxGameTime;
            GameVictory();
        }
    }

    // Part 5. Play Control (Time & Enable)
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
    public void GetExp(int exp)
    {
        if (!mIsLive)
            return;

        mPlayerData.Exp += exp;

        if (mPlayerData.Exp >= mNextExp[Mathf.Min(mPlayerData.Level, mNextExp.Length - 1)])
        {
            mPlayerData.Exp -= mNextExp[Mathf.Min(mPlayerData.Level, mNextExp.Length - 1)];
            mPlayerData.Level++;
            mPlayerData.Health = mPlayerJsonData[mPlayerData.Id].MaxHealth;
            mHUDLevelUp.Show();
        }
    }

    // Part 6. Game Start & End
    IEnumerator GameOverRoutine()
    {
        mIsLive = false;
        yield return mWait0_5s;
        mHUDResult.Lose();
        mHUDResult.gameObject.SetActive(true);
        Stop();
    }
    IEnumerator GameVictoryRoutine()
    {
        mIsLive = false;
        for (int i = 0; i < mEnemyPool.mPool.Count; ++i)
            mEnemyPool.mPool[i].SetActive(false);
        yield return mWait0_5s;
        mHUDResult.Win();
        mHUDResult.gameObject.SetActive(true);
        Stop();
    }
    IEnumerator NoticeRoutine(int i)
    {
        mHUDAchive.gameObject.SetActive(true);
        mHUDAchive.mIcon.sprite = mAchiveSprite[mAchiveJsonData[i].SpriteId];
        mHUDAchive.mText.text = mAchiveJsonData[i].Desc;
        yield return mWait5s;
        mHUDAchive.gameObject.SetActive(false);
    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }
    public void GameStart(int i)
    {
        mPlayerData.Id = i;
        mPlayerData.AnimCtrlId = mPlayerJsonData[i].AnimCtrlId;
        mPlayerData.GameTime = 0;
        mPlayerData.MovementSpeed = mPlayerJsonData[i].MovementSpeed;
        mPlayerData.Health = mPlayerJsonData[i].MaxHealth;
        mPlayerData.Level = 0;
        mPlayerData.Kill = 0;
        mPlayerData.Exp = 0;
        mPlayerData.WeaponSize = 0;
        mPlayerData.PerkSize = 0;

        mPerkCtrlData = new ItemCtrlData[mPlayerJsonData[i].MaxPerkSize];
        mWeaponCtrlData = new ItemCtrlData[mPlayerJsonData[i].MaxWeaponSize];
        mWeaponData = new WeaponData[mPlayerJsonData[i].MaxWeaponSize];
        mWeaponLastData = new WeaponData[mPlayerJsonData[i].MaxWeaponSize];

        mPlayer.gameObject.SetActive(true);
        mHUDLevelUp.Select(mPlayerJsonData[i].StartWeaponId);

        mHUDGameStart.gameObject.SetActive(false);
        mHUDInGame.gameObject.SetActive(true);
        Resume();
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
}
