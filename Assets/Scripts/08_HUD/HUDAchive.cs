using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDAchive : MonoBehaviour
{
    public Image mIcon;
    public TMP_Text mText;

    private void Awake()
    {
        mIcon = GetComponentsInChildren<Image>()[1];
        mText = GetComponentsInChildren<TMP_Text>()[0];
    }
}
