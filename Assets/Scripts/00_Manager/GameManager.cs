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
    [Header("# BGM")]
    public AudioClip mBGMClip;
    public float mBGMVolume;
    public AudioSource mBGMPlayer;
    public AudioHighPassFilter mBGMFffect;
    [Header("# SFX")]
    public AudioClip[] mSFXClip;
    public float mSFXVolume;
    public int mMaxSFXChannel;
    public AudioSource[] mSFXPlayer;
    [Header("# Unity Data Info")]
    public SpriteAtlas mSpriteAtlas;
    public Sprite[] mHUDBtnPlayerSprite;
    public Sprite[] mHUDBtnItemSprite;
    public Sprite[] mHUDAchiveSprite;
    public GameObject[] mPoolPrefabs;   // 프리펩들을 보관할 변수
    public Sprite[] mWeaponSprite;
    public Sprite[] mDropItemSprite;
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
    public PoolManager mDropPool;
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

    void InitDelay()
    {
        mWait0_5s = new WaitForSeconds(0.5f);
        mWait5s = new WaitForSecondsRealtime(5);
    }
    void InitEnemyPool()
    {
        mEnemyPool = new GameObject("EnemyPool").AddComponent<PoolManager>();
        mEnemyPool.transform.parent = GameManager.instance.transform;
        mEnemyPool.Init((int)mEnemyJsonData[0].PrefabType);
    }
    void InitPlayerData()
    {
        // Part 4. Init Player Data
        FuncWeapon.ClearPerk();
        FuncWeapon.UpdatePlayerMovement();
        // Part 5. Init Achive List
        if (!PlayerPrefs.HasKey("MyData"))
        {
            InitAchive();
        }
    }
    void InitBGM()
    {
        // Part 6. Init Audio Player
        mBGMPlayer = new GameObject("BGMPlayer").AddComponent<AudioSource>();
        mBGMPlayer.transform.parent = GameManager.instance.transform;
        mBGMPlayer.playOnAwake = false;
        mBGMPlayer.loop = true;
        mBGMPlayer.volume = mBGMVolume;
        mBGMPlayer.clip = mBGMClip;

        mBGMFffect = Camera.main.GetComponent<AudioHighPassFilter>();
    }
    void InitSFX()
    {
        // Part 6. Init Audio Player
        GameObject sfxPlayer = new GameObject("SFXPlayer");
        sfxPlayer.transform.parent = GameManager.instance.transform;
        mSFXPlayer = new AudioSource[mMaxSFXChannel];
            
        for(int i=0; i<mSFXPlayer.Length; ++i)
        {
            mSFXPlayer[i] = sfxPlayer.AddComponent<AudioSource>();
            mSFXPlayer[i].playOnAwake = false;
            mSFXPlayer[i].loop = false;
            mSFXPlayer[i].bypassListenerEffects = true;
            mSFXPlayer[i].volume = mSFXVolume;
            // mSFXPlayer[i].clip = mSFXClip[i];
        }
            
    }
    // Part 3. Awake
    void Awake()
    {
        instance = this;
        mIsLive = false;
        // Part 1. Json Load
        LoadFromJson();
        InitDelay();
        InitEnemyPool();
        InitPlayerData();
        InitBGM();
        InitSFX();
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

        GameManager.instance.PlayBGM(false);
        GameManager.instance.PlaySFX(Enum.SFX.Lose);
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

        GameManager.instance.PlayBGM(false);
        GameManager.instance.PlaySFX(Enum.SFX.Win);
    }
    IEnumerator NoticeRoutine(int i)
    {
        mHUDAchive.gameObject.SetActive(true);
        mHUDAchive.mIcon.sprite = mHUDAchiveSprite[mAchiveJsonData[i].SpriteId];
        mHUDAchive.mText.text = mAchiveJsonData[i].Desc;
        yield return mWait5s;
        mHUDAchive.gameObject.SetActive(false);

        GameManager.instance.PlaySFX(Enum.SFX.LevelUp);
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

        GameManager.instance.PlayBGM(true);
        GameManager.instance.PlaySFX(Enum.SFX.Select);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    // Part 7. Sound Control
    public void PlayBGM(bool en)
    {
        if (en)
        {
            mBGMPlayer.Play();
        }
        else
        {
            mBGMPlayer.Stop();
        }
    }
    public void PlayEffect(bool en)
    {
        mBGMFffect.enabled = en;
    }
    public void PlaySFX(Enum.SFX sfx)
    {
        for(int i=0; i < mSFXPlayer.Length; ++i)
        {
            if (mSFXPlayer[i].isPlaying)
                continue;
            mSFXPlayer[i].clip = mSFXClip[(int)sfx];
            mSFXPlayer[i].Play();
            break;
        }
    }
}
