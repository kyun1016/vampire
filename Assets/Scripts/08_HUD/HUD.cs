using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health}
    public InfoType mType;

    TMP_Text mText;
    Slider mSlider;

    private void Awake()
    {
        mText = GetComponent<TMP_Text>();
        mSlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (mType){
            case InfoType.Exp:
                float curExp = GameManager.instance.mExp;
                float maxExp = GameManager.instance.mNextExp[Mathf.Min(GameManager.instance.mLevel, GameManager.instance.mNextExp.Length - 1)];
                mSlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                mText.text = string.Format("Lv.{0:F0}", GameManager.instance.mLevel);
                break;
            case InfoType.Kill:
                mText.text = string.Format("{0:F0}", GameManager.instance.mKill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.mMaxGameTime - GameManager.instance.mGameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                mText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.mHealth;
                float maxHealth = GameManager.instance.mMaxHealth;
                mSlider.value = curHealth / maxHealth;
                break;
        }
    }
}
