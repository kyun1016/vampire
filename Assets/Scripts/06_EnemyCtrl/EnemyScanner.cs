using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    public float mScanRange;
    public LayerMask mTargetLayer;
    public RaycastHit2D[] mTargets;
    public Transform mNearestTarget;

    private void FixedUpdate()
    {
        mTargets = Physics2D.CircleCastAll(transform.position, mScanRange, Vector2.zero, 0, mTargetLayer);
        mNearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in mTargets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;

            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
