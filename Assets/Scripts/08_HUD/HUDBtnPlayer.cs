using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDBtnPlayer : MonoBehaviour
{
    public int mId;

    Image mImgBackground;
    Image mImgChar;
    TMP_Text mTextName;
    TMP_Text mTextDesc;

    private void Awake()
    {
        Image[] imgs = GetComponentsInChildren<Image>();
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        mImgBackground = imgs[0];
        mImgChar = imgs[1];
        mTextName = texts[0];
        mTextDesc = texts[1];
    }

    public void UpdateText()
    {
        if (!GameManager.instance.mPlayerJsonData[mId].Enable)
        {
            mTextName.text = GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].PlayerUnlockName[mId];
            mTextDesc.text = GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].PlayerUnlockDesc[mId];
        }
        else
        {
            mTextName.text = GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].PlayerName[mId];
            mTextDesc.text = GameManager.instance.mTextJsonData[(int)GameManager.instance.mSettingData.LanguageType].PlayerDesc[mId];
        }
    }

    public void Init()
    {
        UpdateText();
        mImgChar.sprite = GameManager.instance.mHUDBtnPlayerSprite[mId];
        if (!GameManager.instance.mPlayerJsonData[mId].Enable)
        {
            mImgBackground.color = new Color(150, 150, 150);
            mImgChar.color = new Color(0, 0, 0);
        }
    }

    public void OnClick()
    {
        GameManager.instance.GameStart(mId);
    }
}
