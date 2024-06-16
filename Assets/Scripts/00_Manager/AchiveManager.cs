using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    Enum.Achive[] achives;

    private void Awake()
    {
        achives = new Enum.Achive[System.Enum.GetValues(typeof(Enum.Achive)).Length];

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }
    public void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach(Enum.Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    void CheckAchive()
    {
        foreach (Enum.Achive achive in achives)
        {
            switch (achive)
            {
                case Enum.Achive.UnlockChar2:
                    if (GameManager.instance.mPlayerData.Kill >= 10)
                        PlayerPrefs.SetInt(achive.ToString(), 1);
                    break;
                case Enum.Achive.UnlockChar3:
                    if (GameManager.instance.mPlayerData.GameTime >= 300)
                        PlayerPrefs.SetInt(achive.ToString(), 1);
                    break;
                default:
                    Debug.Assert(false, "Error");
                    break;
            }
        }
    }
}
