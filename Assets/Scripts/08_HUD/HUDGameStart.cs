using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDGameStart : MonoBehaviour
{
    public GameObject mTemplate;
    public TMP_Text mTextPowerUp;
    GameObject[] mPlayers;

    public void UpdateText()
    {
        mTextPowerUp.text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].HUDGameStart[0];
        for (int i = 0; i < mPlayers.Length; ++i)
        {
            mPlayers[i].GetComponent<HUDBtnPlayer>().UpdateText();
        }
    }

    public void Init()
    {
        mPlayers = new GameObject[GameManager.instance.mJsonPlayerData.Length];
        mPlayers[0] = mTemplate;
        mPlayers[0].GetComponent<HUDBtnPlayer>().Init(0);
        for (int i = 1; i < mPlayers.Length; ++i)
        {
            mPlayers[i] = Instantiate(mTemplate);
            mPlayers[i].transform.name = "Character " + i;
            mPlayers[i].transform.SetParent(mTemplate.transform.parent);
            mPlayers[i].transform.localScale = mTemplate.transform.localScale;
            mPlayers[i].GetComponent<HUDBtnPlayer>().Init(i);
            mPlayers[i].GetComponent<Button>().interactable = true;
            if (!GameManager.instance.mJsonPlayerData[i].Enable)
                mPlayers[i].GetComponent<Button>().interactable = false;
        }
    }
}
