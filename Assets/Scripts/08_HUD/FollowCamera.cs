using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
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
