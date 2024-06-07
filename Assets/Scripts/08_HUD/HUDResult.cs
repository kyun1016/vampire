using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDResult : MonoBehaviour
{
    public GameObject[] mTitle;
    public void Lose()
    {
        mTitle[0].SetActive(true);
        mTitle[1].SetActive(false);
    }
    public void Win()
    {
        mTitle[0].SetActive(false);
        mTitle[1].SetActive(true);
    }
}
