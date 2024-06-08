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
    public void Init()
    {
        mImgChar.sprite = GameManager.instance.mHUDBtnPlayerSprite[mId];
        mTextName.text = GameManager.instance.mPlayerJsonData[mId].Name;
        mTextDesc.text = GameManager.instance.mPlayerJsonData[mId].Desc;
        if (!GameManager.instance.mPlayerJsonData[mId].Enable)
        {
            mTextName.text = "Unlocked";
            mTextDesc.text = GameManager.instance.mPlayerJsonData[mId].UnlockDesc;
            mImgBackground.color = new Color(150, 150, 150);
            mImgChar.color = new Color(0, 0, 0);
        }
    }

    public void OnClick()
    {
        GameManager.instance.GameStart(mId);
    }
}
