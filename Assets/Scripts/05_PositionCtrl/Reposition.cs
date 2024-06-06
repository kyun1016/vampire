using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D mColl;

    void Awake()
    {
        mColl = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("AreaCollider"))
            return;

        Vector3 playerPos = GameManager.instance.mPlayer.transform.position;
        Vector3 myPos = transform.position;

        float diffX = playerPos.x - myPos.x;
        float diffY = playerPos.y - myPos.y;

        Vector3 playerDir = GameManager.instance.mPlayer.mLastDir;

        switch (transform.tag)
        {
            case "Ground":
                if (Mathf.Abs(diffX) > Mathf.Abs(diffY))
                {
                    if (diffX > 0)
                        transform.Translate(Vector3.right * 80);
                    else
                        transform.Translate(Vector3.right * -80);
                }
                else
                {
                    if (diffY > 0)
                        transform.Translate(Vector3.up * 80);
                    else
                        transform.Translate(Vector3.up * -80);
                }
                break;
            case "Enemy":
                if (mColl.enabled)
                {
                    transform.Translate(playerDir * 40 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;

        }
    }
}
