using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerCtrl mPlayer;

    public static GameManager mInstance;

    void Awake()
    {
        mInstance = this;
    }
}
