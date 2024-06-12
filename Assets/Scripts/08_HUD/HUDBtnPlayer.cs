using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDBtnPlayer : MonoBehaviour
{
    public int mId;

    public Image mImgBackground;
    public Image mImgChar;
    public TMP_Text mTextName;
    public TMP_Text mTextDesc;


    public void UpdateText()
    {
        if (!GameManager.instance.mJsonPlayerData[mId].Enable)
        {
            mTextName.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].PlayerUnlockName[mId];
            mTextDesc.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].PlayerUnlockDesc[mId];
        }
        else
        {
            mTextName.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].PlayerName[mId];
            mTextDesc.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].PlayerDesc[mId];
        }
    }

    public void Init(int id)
    {
        mId = id;
        UpdateText();
        mImgChar.sprite = GameManager.instance.mHUDBtnPlayerSprite[mId];
        if (!GameManager.instance.mJsonPlayerData[mId].Enable)
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
