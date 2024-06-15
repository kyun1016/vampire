using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDPanelPowerUpDisplay : MonoBehaviour
{
    public int mId;

    public Image mIcon;
    public TMP_Text mTextName;
    public TMP_Text mTextDesc;
    public TMP_Text mTextBuy;
    public TMP_Text mTextGold;

    public void UpdateText()
    {
        mTextName.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].HUDPowerUpName[mId];
        mTextDesc.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].HUDPowerUpDesc[mId];
        mTextBuy.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].HUDPowerUpBuy[0];
        mTextGold.text = GameManager.instance.mJsonPowerUpData[mId].GoldCost[0].ToString();
    }

    void UpdateSprite()
    {
        mIcon.sprite = GameManager.instance.mHUDPowerUpSprite[mId];
    }
    public void Init(int id)
    {
        mId = id;
        UpdateText();
        UpdateSprite();
    }

    public void OnClick()
    {

    }
}
