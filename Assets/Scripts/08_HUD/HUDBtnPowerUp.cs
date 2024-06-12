using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDBtnPowerUp : MonoBehaviour
{
    public int mId;

    public Image mIcon;
    public GameObject mCheckBoxTemplet;
    GameObject[] mCheckBox;
    public TMP_Text mTextName;
    TMP_Text mTextDesc;

    public void UpdateText()
    {
        mTextName.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].HUDPowerUpName[mId];
        // mTextDesc.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].HUDPowerUpDesc[mId];
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

    private void OnEnable()
    {
        UpdateText();
        UpdateSprite();
    }

    public void OnClick()
    {

    }
}
