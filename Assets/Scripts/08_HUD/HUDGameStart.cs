using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDGameStart : MonoBehaviour
{
    RectTransform mRect;
    public GameObject mRootHUD;
    GameObject[] mPlayers;

    public void UpdateText()
    {
        for (int i = 0; i < mPlayers.Length; ++i)
        {
            mPlayers[i].GetComponent<HUDBtnPlayer>().UpdateText();
        }
    }

    void Awake()
    {
        mRect = GetComponent<RectTransform>();
        mPlayers = new GameObject[GameManager.instance.mPlayerJsonData.Length];
        mPlayers[0] = mRootHUD;
        mPlayers[0].GetComponent<HUDBtnPlayer>().mId = 0;
        mPlayers[0].GetComponent<HUDBtnPlayer>().Init();
        for (int i = 1; i < mPlayers.Length; ++i)
        {
            mPlayers[i] = Instantiate(mRootHUD);
            mPlayers[i].transform.SetParent(mRootHUD.transform.parent);
            mPlayers[i].transform.name = "Character " + i;
            mPlayers[i].transform.localScale = mRootHUD.transform.localScale;
            mPlayers[i].GetComponent<HUDBtnPlayer>().mId = i;
            mPlayers[i].GetComponent<HUDBtnPlayer>().Init();
            mPlayers[i].GetComponent<Button>().interactable = true;
            if (!GameManager.instance.mPlayerJsonData[i].Enable)
                mPlayers[i].GetComponent<Button>().interactable = false;
        }    
    }
}
