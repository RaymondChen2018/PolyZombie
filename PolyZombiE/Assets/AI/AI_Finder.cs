using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventTranform : UnityEvent<Transform>
{

}
public class AI_Finder : MonoBehaviour {
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] private LayerMask losObstacleLayerMask;
    [SerializeField] private float viewDist = 10.0f;
    [SerializeField] private float viewConeAngle = 60.0f;
    [SerializeField] private List<Transform> targetInSight;
    //private Transform closestTargetInSight = null;

    [SerializeField] private UnityEventTranform OnSeeEveryTarget = new UnityEventTranform();
    [SerializeField] private UnityEventInt OnSeeTargets = new UnityEventInt();

    [Header("Debug")]
    [SerializeField] private bool debugOn = false;
    [SerializeField] private Color debugColor = Color.red;

    // Use this for initialization
    void Start () {
        targetInSight = new List<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 thisPos = transform.position;
        Vector2 thisDOF = transform.right;

        // Range detection
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(thisPos, viewDist, targetLayerMask);

        // View Cone detection
        targetInSight.Clear();

        for (int i = 0; i < enemyInRange.Length; i++)
        {
            Collider2D enemyCollider = enemyInRange[i];
            Vector2 enemyPos = enemyCollider.transform.position;

            // Test view cone
            bool inViewCone = enemyInViewCone(enemyPos, thisPos, thisDOF, viewConeAngle);

            // Test Line of sight
            bool inLOS = enemyInLOS(enemyPos, thisPos, losObstacleLayerMask);

            if (inViewCone && inLOS)
            {
                targetInSight.Add(enemyCollider.transform);
                Func_OnSeeEveryTarget(enemyCollider.transform);
            }
        }
        OnSeeTargets.Invoke(targetInSight.Count);
        // Find closest enemy
        //if (targetInSight.Count > 0)
        //{
        //    Transform tmp = targetInSight[0].transform;
        //    Transform ret = tmp;
        //    float closestDistSqrTmp = ((Vector2)tmp.position - thisPos).sqrMagnitude;
        //    float closestDistSqr = closestDistSqrTmp;
        //    for (int i = 1; i < targetInSight.Count; i++)
        //    {
        //        tmp = targetInSight[i].transform;
        //        closestDistSqrTmp = ((Vector2)tmp.position - thisPos).sqrMagnitude;
        //        if (closestDistSqr > closestDistSqrTmp)
        //        {
        //            closestDistSqr = closestDistSqrTmp;
        //            ret = tmp;
        //        }
        //    }
        //    if (closestTargetInSight != ret)
        //    {
        //        Func_OnSeeCloserTarget(ret);
        //    }
        //    closestTargetInSight = ret;
        //}
        //else
        //{
        //    closestTargetInSight = null;
        //}

        // Debug
        debug();
    }

    private void Func_OnSeeEveryTarget(Transform targetTransform)
    {
        // Call back
        OnSeeEveryTarget.Invoke(targetTransform);
    }
    //private void Func_OnSeeCloserTarget(Transform targetTransform)
    //{
    //    // Call back
    //    OnFoundCloserTarget.Invoke(targetTransform);
    //}

    //public Transform getClosestTargetInsight()
    //{
    //    return closestTargetInSight;
    //}
    public List<Transform> getSightCache()
    {
        for(int i = 0; i < targetInSight.Count; i++)
        {
            if(targetInSight[i] == null)
            {
                targetInSight.RemoveAt(i);
                i--;
            }
        }
        return targetInSight;
    }
    bool enemyInLOS(Vector2 enemyPos, Vector2 thisPos, LayerMask obstacleMask)
    {
        return !Physics2D.Linecast(enemyPos, thisPos, losObstacleLayerMask);
    }
    bool enemyInViewCone(Vector2 enemyPos, Vector2 thisPos, Vector2 thisDOF, float viewCone)
    {
        bool inViewCone = false;
        Vector2 enemyDirection = (enemyPos - thisPos).normalized;
        //float dot = thisDOF.x * enemyDirection.x + thisDOF.y * enemyDirection.y;
        //float dotViewCone = 1.0f - viewCone / 180.0f;

        //if (dot > dotViewCone)
        //{
        //    inViewCone = true;
        //}

        if (Vector2.Angle(thisDOF, enemyDirection) < viewCone / 2.0f)
        {
            inViewCone = true;
        };
        return inViewCone;
    }

    public static void DrawEllipse(Vector3 pos, float radius, Color color)
    {
        int segments = 32;
        float angle = 0f;
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        Vector3 lastPoint = Vector3.zero;
        Vector3 thisPoint = Vector3.zero;

        for (int i = 0; i < segments + 1; i++)
        {
            thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            if (i > 0)
            {
                Debug.DrawLine(rot * lastPoint + pos, rot * thisPoint + pos, color);
            }

            lastPoint = thisPoint;
            angle += 360f / segments;
        }
    }

    void debug()
    {
        if (!debugOn)
        {
            return;
        }

        Vector2 thisPos = transform.position;
        Vector2 thisDOF = transform.right;
        Vector2 leftViewBound = Quaternion.AngleAxis(viewConeAngle / 2.0f, Vector3.forward) * thisDOF;
        Vector2 rightViewBound = Quaternion.AngleAxis(-viewConeAngle / 2.0f, Vector3.forward) * thisDOF;

        DrawEllipse(thisPos, viewDist, debugColor);
        Debug.DrawLine(thisPos, thisDOF * viewDist + thisPos, debugColor); // DOF
        Debug.DrawLine(thisPos, leftViewBound * viewDist + thisPos, debugColor); // Left view bound
        Debug.DrawLine(thisPos, rightViewBound * viewDist + thisPos, debugColor); // Right view bound

        // Detected targets
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(thisPos, viewDist, targetLayerMask);
        for (int i = 0; i < enemyInRange.Length; i++)
        {
            Collider2D enemyCollider = enemyInRange[i];
            Vector2 enemyPos = enemyCollider.transform.position;

            // Test view cone
            bool inViewCone = enemyInViewCone(enemyPos, thisPos, thisDOF, viewConeAngle);

            // Test Line of sight
            bool inLOS = enemyInLOS(enemyPos, thisPos, losObstacleLayerMask);

            if (inViewCone && inLOS)
            {
                Color tmp = debugColor;
                tmp.r = 1.0f - debugColor.r;
                tmp.g = 1.0f - debugColor.g;
                tmp.b = 1.0f - debugColor.b;
                Debug.DrawLine(thisPos, enemyPos, tmp);
            }
        }
    }
    void OnDrawGizmos()
    {
        debug();
    }
}
