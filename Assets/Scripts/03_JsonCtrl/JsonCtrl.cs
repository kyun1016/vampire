using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using TMPro;

public class PlayerInfo
{
    public string mMainName;
    public string mSubName;
    public string mDescription;
}

public class PlayerGroup
{
    public PlayerInfo A;

}

public class JsonCtrl : MonoBehaviour
{
    public void PlayerInfoToJson(PlayerInfo info)
    {
        string jsonData = JsonUtility.ToJson(info);
    }
}
