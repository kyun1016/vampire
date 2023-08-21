using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BtnSceneChange : MonoBehaviour
{
    public void ButtonClick(int targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
}
