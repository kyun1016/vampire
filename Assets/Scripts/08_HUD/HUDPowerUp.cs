using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDPowerUp : MonoBehaviour
{
    public GameObject mBtnTemplet;
    GameObject[] mItems;
    public TMP_Text mTextTitle;

    public void UpdateText()
    {
        mTextTitle.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].HUDLevelUpTitle[0];
        for (int i = 0; i < mItems.Length; ++i)
        {
            mItems[i].GetComponent<HUDBtnPowerUp>().UpdateText();
        }
    }

    public void Init()
    {
        mItems = new GameObject[GameManager.instance.mJsonPowerUpData.Length];
        mItems[0] = mBtnTemplet;
        mItems[0].GetComponent<HUDBtnPowerUp>().Init(0);
        for (int i = 1; i < mItems.Length; ++i)
        {
            mItems[i] = Instantiate(mBtnTemplet);
            mItems[i].GetComponent<HUDBtnPowerUp>().Init(i);
            mItems[i].transform.SetParent(mBtnTemplet.transform.parent);
            mItems[i].transform.name = "BtnPowerUp" + i;
        }
    }
}
