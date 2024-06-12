using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDInGame : MonoBehaviour
{
    TMP_Text mTextKill;
    TMP_Text mTextGold;
    TMP_Text mTextLevel;
    TMP_Text mTextTime;
    Slider mSliderExp;
    Slider mSliderHealth;

    private void Awake()
    {
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        Slider[] sliders = GetComponentsInChildren<Slider>();
        mTextKill = texts[0];
        mTextGold = texts[1];
        mTextLevel = texts[2];
        mTextTime = texts[3];
        mSliderExp = sliders[0];
        mSliderHealth = sliders[1];
    }

    private void LateUpdate()
    {
        // Kill
        mTextKill.text = string.Format("{0:F0}", GameManager.instance.mPlayerData.Kill);
        // Gold
        mTextGold.text = string.Format("{0:F0}", GameManager.instance.mPlayerData.Gold);
        // Level
        mTextLevel.text = string.Format("Lv.{0:F0}", GameManager.instance.mPlayerData.Level);
        // Time
        float remainTime = GameManager.instance.mJsonPlayerData[GameManager.instance.mPlayerData.Id].MaxGameTime - GameManager.instance.mPlayerData.GameTime;
        int min = Mathf.FloorToInt(remainTime / 60);
        int sec = Mathf.FloorToInt(remainTime % 60);
        mTextTime.text = string.Format("{0:D2}:{1:D2}", min, sec);
        // Exp
        float curExp = GameManager.instance.mPlayerData.Exp;
        float maxExp = GameManager.instance.mJsonLevelData.PlayerExp[Mathf.Min(GameManager.instance.mPlayerData.Level, GameManager.instance.mJsonLevelData.PlayerExp.Length - 1)];
        mSliderExp.value = curExp / maxExp;
        // Health
        float curHealth = GameManager.instance.mPlayerData.Health;
        float maxHealth = GameManager.instance.mJsonPlayerData[GameManager.instance.mPlayerData.Id].MaxHealth;
        mSliderHealth.value = curHealth / maxHealth;
    }
}
