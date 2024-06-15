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
    public bool mIsLive = false;
    [Header("# Setting")]
    public SettingData mSettingData;
    [Header("# BGM")]
    public AudioClip mBGMClip;
    public AudioSource mBGMPlayer;
    public AudioHighPassFilter mBGMFffect;
    [Header("# SFX")]
    public AudioClip[] mSFXClip;
    public int mMaxSFXChannel;
    public AudioSource[] mSFXPlayer;
    [Header("# Unity Data Info")]
    public SpriteAtlas mSpriteAtlas;
    public Sprite[] mHUDBtnPlayerSprite;
    public Sprite[] mHUDBtnItemSprite;
    public Sprite[] mHUDAchiveSprite;
    public Sprite[] mHUDPowerUpSprite;
    public GameObject[] mPoolPrefabs;   // 프리펩들을 보관할 변수
    public Sprite[] mWeaponSprite;
    public Sprite[] mFieldObjectSprite;
    public Sprite[] mDropItemSprite;
    public RuntimeAnimatorController[] mPlayerAnimCtrl;
    public RuntimeAnimatorController[] mEnemyAnimCtrl;
    [Header("# Json Info")]
    public JsonFieldObjectData mJsonFieldObjectData;
    public JsonTextData[] mJsonTextData;
    public JsonAchiveData[] mJsonAchiveData;
    public JsonLevelData mJsonLevelData;
    public JsonPlayerData[] mJsonPlayerData;
    public JsonWeaponData[] mJsonWeaponData;
    public JsonPerkData[] mJsonPerkData;
    public JsonPowerUpData[] mJsonPowerUpData;
    public JsonEnemyData[] mJsonEnemyData;
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
    public PoolManager mFieldObjectPool;
    public HUDGameStart mHUDGameStart;
    public HUDAchive mHUDAchive;
    public HUDInGame mHUDInGame;
    public HUDLevelUp mHUDLevelUp;
    public HUDResult mHUDResult;
    public HUDSetting mHUDSetting;
    public HUDPowerUp mHUDPowerUp;
    public WaitForSeconds mWait0_5s;
    public WaitForSecondsRealtime mWait5s;

    // Part 1. Achive
    public void InitAchive()
    {
        PlayerPrefs.SetInt("MyData", 1);

        for (int i = 0; i < mJsonAchiveData.Length; ++i)
        {
            PlayerPrefs.SetInt(mJsonAchiveData[i].Name.ToString(), 0);
        }
    }
    void CheckAchive()
    {
        for (int i = 0; i < mJsonAchiveData.Length; ++i)
        {
            bool isAchive = false;
            switch (mJsonAchiveData[i].Name)
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
            if (isAchive && PlayerPrefs.GetInt(mJsonAchiveData[i].Name.ToString()) == 0)
            {
                PlayerPrefs.SetInt(mJsonAchiveData[i].Name.ToString(), 1);
                StartCoroutine(NoticeRoutine(i));
            }
        }
    }
    // Part 2. Json
    public void SaveSettingJson()
    {
        string json = JsonConvert.SerializeObject(mSettingData);
        File.WriteAllText(Application.dataPath + "/Datas/SettingData.json", json);
    }
    public void SaveToJson()
    {
        string json = JsonConvert.SerializeObject(mJsonEnemyData);
        File.WriteAllText(Application.dataPath + "/Datas/JsonEnemyData.json", json);
        json = JsonConvert.SerializeObject(mJsonWeaponData);
        File.WriteAllText(Application.dataPath + "/Datas/JsonWeaponData.json", json);
        json = JsonConvert.SerializeObject(mJsonPerkData);
        File.WriteAllText(Application.dataPath + "/Datas/JsonPerkData.json", json);
        json = JsonConvert.SerializeObject(mJsonPlayerData);
        File.WriteAllText(Application.dataPath + "/Datas/JsonPlayerData.json", json);
        json = JsonConvert.SerializeObject(mJsonAchiveData);
        File.WriteAllText(Application.dataPath + "/Datas/JsonAchiveData.json", json);
        json = JsonConvert.SerializeObject(mJsonTextData);
        File.WriteAllText(Application.dataPath + "/Datas/JsonTextData.json", json);
        json = JsonConvert.SerializeObject(mJsonLevelData);
        File.WriteAllText(Application.dataPath + "/Datas/JsonLevelData.json", json);
        json = JsonConvert.SerializeObject(mJsonFieldObjectData);
        File.WriteAllText(Application.dataPath + "/Datas/JsonFieldObjectData.json", json);
        json = JsonConvert.SerializeObject(mJsonPowerUpData);
        File.WriteAllText(Application.dataPath + "/Datas/JsonPowerUpData.json", json);

        SaveSettingJson();
    }
    public void LoadSettingJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/Datas/SettingData.json");
        mSettingData = JsonConvert.DeserializeObject<SettingData>(json);
    }
    private void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/Datas/JsonEnemyData.json");
        mJsonEnemyData = JsonConvert.DeserializeObject<JsonEnemyData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/JsonPlayerData.json");
        mJsonPlayerData = JsonConvert.DeserializeObject<JsonPlayerData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/JsonWeaponData.json");
        mJsonWeaponData = JsonConvert.DeserializeObject<JsonWeaponData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/JsonPerkData.json");
        mJsonPerkData = JsonConvert.DeserializeObject<JsonPerkData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/JsonAchiveData.json");
        mJsonAchiveData = JsonConvert.DeserializeObject<JsonAchiveData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/JsonLevelData.json");
        mJsonLevelData = JsonConvert.DeserializeObject<JsonLevelData>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/JsonPowerUpData.json");
        mJsonPowerUpData = JsonConvert.DeserializeObject<JsonPowerUpData[]>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/JsonFieldObjectData.json");
        mJsonFieldObjectData = JsonConvert.DeserializeObject<JsonFieldObjectData>(json);

        json = File.ReadAllText(Application.dataPath + "/Datas/JsonTextData.json");
        mJsonTextData = JsonConvert.DeserializeObject<JsonTextData[]>(json);

        Debug.Assert(mJsonTextData[0].HUDPowerUpName.Length == mJsonPowerUpData.Length, "Power Up Count Error");

        LoadSettingJson();
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
        mEnemyPool.Init((int)mJsonEnemyData[0].PrefabEnemy);
    }
    void InitDropPool()
    {
        mDropPool = new GameObject("DropPool").AddComponent<PoolManager>();
        mDropPool.transform.parent = GameManager.instance.transform;
        mDropPool.Init((int)Enum.PrefabType.DropItem);
    }
    void InitFieldObjectPool()
    {
        mFieldObjectPool = new GameObject("FieldObjectPool").AddComponent<PoolManager>();
        mFieldObjectPool.transform.parent = GameManager.instance.transform;
        mFieldObjectPool.Init((int)Enum.PrefabType.FieldObject);
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
        mBGMPlayer.volume = mSettingData.BGMVolume * mSettingData.MasterVolume;
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
            mSFXPlayer[i].volume = mSettingData.SFXVolume * mSettingData.MasterVolume;
            // mSFXPlayer[i].clip = mSFXClip[i];
        }       
    }
    void InitWindowScreen()
    {

    }
    void InitHUDGameStart()
    {
        mHUDGameStart.gameObject.SetActive(true);
        mHUDGameStart.Init();
    }
    void InitHUDLevelUp()
    {
        // mHUDLevelUp.gameObject.SetActive(true);
        mHUDLevelUp.Init();
        mHUDLevelUp.gameObject.SetActive(false);
    }
    void InitHUDSetting()
    {
        mHUDSetting.gameObject.SetActive(true);
        mHUDSetting.Init();
        mHUDSetting.gameObject.SetActive(false);
    }
    void InitHUDPowerUp()
    {
        mHUDPowerUp.gameObject.SetActive(true);
        mHUDPowerUp.Init();
        mHUDPowerUp.gameObject.SetActive(false);
    }
    void Init()
    {
        instance = this;
        mIsLive = false;
        // Part 1. Json Load
        LoadFromJson();
        InitWindowScreen();
        InitDelay();

        InitEnemyPool();
        InitDropPool();
        InitFieldObjectPool();

        InitPlayerData();
        InitBGM();
        InitSFX();
        InitHUDGameStart();
        InitHUDLevelUp();
        InitHUDPowerUp();
        InitHUDSetting();
    }
    // Part 3. Awake
    void Awake()
    {
        Init();
        Debug.Assert(mFieldObjectSprite.Length == System.Enum.GetValues(typeof(Enum.FieldObjectSprite)).Length, "Error");
        // Debug.Assert(mWeaponSprite.Length == System.Enum.GetValues(typeof(Enum.WeaponType)).Length, "Error");
        Debug.Assert(mPlayerAnimCtrl.Length == mJsonPlayerData.Length, "Error");

    }

    public void HUDSettingToggle()
    {
        if (mHUDSetting.gameObject.activeSelf)
        {
            mHUDSetting.gameObject.SetActive(false);
            GameManager.instance.PlayEffect(false);
            Time.timeScale = 1;
        }
        else
        {
            mHUDSetting.gameObject.SetActive(true);
            GameManager.instance.PlayEffect(true);
            Time.timeScale = 0;
        }
    }

    public void HUDPowerUpToggle()
    {
        if (mHUDPowerUp.gameObject.activeSelf)
        {
            mHUDPowerUp.gameObject.SetActive(false);
            GameManager.instance.PlayEffect(false);
            Time.timeScale = 1;
        }
        else
        {
            mHUDPowerUp.gameObject.SetActive(true);
            GameManager.instance.PlayEffect(true);
            Time.timeScale = 0;
        }
    }

    // Part 4. Update
    void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mHUDPowerUp.gameObject.activeSelf)
                HUDPowerUpToggle();
            else
                HUDSettingToggle();
            SaveSettingJson();
        }

        if(mHUDLevelUp.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                mHUDLevelUp.SelectBtn(false);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                mHUDLevelUp.SelectBtn(true);
            }
        }
    }
    public void UpdateText()
    {
        mHUDGameStart.UpdateText();
        mHUDLevelUp.UpdateText();
        mHUDSetting.UpdateText();
    }

    void Update()
    {
        InputCheck();
        if (!mIsLive)
            return;

        mPlayerData.GameTime += Time.deltaTime;
        CheckAchive();

        if (mPlayerData.GameTime > mJsonPlayerData[mPlayerData.Id].MaxGameTime)
        {
            mPlayerData.GameTime = mJsonPlayerData[mPlayerData.Id].MaxGameTime;
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
    public void GetExp(int value)
    {
        if (!mIsLive)
            return;

        mPlayerData.Exp += value;

        if (mPlayerData.Exp >= mJsonLevelData.PlayerExp[Mathf.Min(mPlayerData.Level, mJsonLevelData.PlayerExp.Length - 1)])
        {
            mPlayerData.Exp -= mJsonLevelData.PlayerExp[Mathf.Min(mPlayerData.Level, mJsonLevelData.PlayerExp.Length - 1)];
            mPlayerData.Level++;
            mHUDLevelUp.gameObject.SetActive(true);
            mHUDLevelUp.Show();
        }
    }
    public void GetGold(int value)
    {
        if (!mIsLive)
            return;

        mPlayerData.Gold += value;
        mSettingData.TotalGold += value;
    }
    public void GetHealth(int value)
    {
        if (!mIsLive)
            return;
        mPlayerData.Health += value;
        mPlayerData.Health = mPlayerData.Health < mPlayerData.MaxHealth ? mPlayerData.Health : mPlayerData.MaxHealth;
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
        mHUDAchive.mIcon.sprite = mHUDAchiveSprite[mJsonAchiveData[i].SpriteId];
        mHUDAchive.mText.text = mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].AchiveDesc[i];
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
        mPlayerData.AnimCtrlId = mJsonPlayerData[i].AnimCtrlId;
        mPlayerData.GameTime = 0;
        mPlayerData.MovementSpeed = mJsonPlayerData[i].MovementSpeed;
        mPlayerData.MaxHealth = mJsonPlayerData[i].MaxHealth;
        mPlayerData.Health = mJsonPlayerData[i].MaxHealth;
        mPlayerData.Level = 0;
        mPlayerData.Kill = 0;
        mPlayerData.Exp = 0;
        mPlayerData.WeaponSize = 0;
        mPlayerData.PerkSize = 0;

        mPerkCtrlData = new ItemCtrlData[mJsonPlayerData[i].MaxPerkSize];
        mWeaponCtrlData = new ItemCtrlData[mJsonPlayerData[i].MaxWeaponSize];
        mWeaponData = new WeaponData[mJsonPlayerData[i].MaxWeaponSize];
        mWeaponLastData = new WeaponData[mJsonPlayerData[i].MaxWeaponSize];

        mPlayer.gameObject.SetActive(true);

        mHUDLevelUp.gameObject.SetActive(true);
        mHUDLevelUp.Select(mJsonPlayerData[i].StartWeaponId);
        mHUDLevelUp.gameObject.SetActive(false);

        mHUDGameStart.gameObject.SetActive(false);
        mHUDInGame.gameObject.SetActive(true);
        Resume();

        GameManager.instance.PlayBGM(true);
        GameManager.instance.PlaySFX(Enum.SFX.Select);
    }
    public void GameRetry()
    {
        SaveSettingJson();
        SceneManager.LoadScene(0);
        Init();
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
