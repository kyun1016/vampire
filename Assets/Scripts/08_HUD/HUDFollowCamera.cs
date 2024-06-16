using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDFollowCamera : MonoBehaviour
{
    RectTransform mRect;

    private void Awake()
    {
        mRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mRect.position = Camera.main.WorldToScreenPoint(GameManager.instance.mPlayer.transform.position);
    }
}
