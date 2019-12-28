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
    [SerializeField] protected Movement movement;
    [SerializeField] protected Orient orient;
    [SerializeField] private List<Transform> targetInSight;
    private Transform closestTargetInSight = null;

    [SerializeField] private UnityEventTranform OnSeeEveryTarget = new UnityEventTranform();
    [SerializeField] private UnityEventInt OnSeeTargets = new UnityEventInt();
    [SerializeField] private UnityEventTranform OnFoundCloserTarget = new UnityEventTranform();

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(movement);
        Assert.IsNotNull(orient);

        targetInSight = new List<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        // Range detection
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(movement.getPosition(), viewDist, targetLayerMask);
        DrawEllipse(transform.position, viewDist, Color.yellow);

        // View Cone detection
        targetInSight.Clear();

        for (int i = 0; i < enemyInRange.Length; i++)
        {
            Collider2D enemyCollider = enemyInRange[i];
            Vector2 enemyPos = enemyCollider.transform.position;
            Vector2 thisPos = movement.getPosition();
            Vector2 thisDOF = orient.GetDOF();

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
        if (targetInSight.Count > 0)
        {
            Transform tmp = targetInSight[0].transform;
            Transform ret = tmp;
            float closestDistSqrTmp = ((Vector2)tmp.position - movement.getPosition()).sqrMagnitude;
            float closestDistSqr = closestDistSqrTmp;
            for (int i = 1; i < targetInSight.Count; i++)
            {
                tmp = targetInSight[i].transform;
                closestDistSqrTmp = ((Vector2)tmp.position - movement.getPosition()).sqrMagnitude;
                if (closestDistSqr > closestDistSqrTmp)
                {
                    closestDistSqr = closestDistSqrTmp;
                    ret = tmp;
                }
            }
            if(closestTargetInSight != ret)
            {
                Func_OnSeeCloserTarget(ret);
            }
            closestTargetInSight = ret;
        }
        else
        {
            closestTargetInSight = null;
        }

        // Child class update
        UpdateDerived();
    }
    virtual protected void UpdateDerived() { }
    private void Func_OnSeeEveryTarget(Transform targetTransform)
    {
        // Call back
        OnSeeEveryTarget.Invoke(targetTransform);
    }
    private void Func_OnSeeCloserTarget(Transform targetTransform)
    {
        // Call back
        OnFoundCloserTarget.Invoke(targetTransform);
    }

    public Transform getClosestTargetInsight()
    {
        return closestTargetInSight;
    }

    bool enemyInLOS(Vector2 enemyPos, Vector2 thisPos, LayerMask obstacleMask)
    {
        return !Physics2D.Linecast(enemyPos, movement.getPosition(), losObstacleLayerMask);
    }
    bool enemyInViewCone(Vector2 enemyPos, Vector2 thisPos, Vector2 thisDOF, float viewCone)
    {
        bool inViewCone = false;
        Vector2 enemyDirection = (enemyPos - thisPos).normalized;
        float dot = thisDOF.x * enemyDirection.x + thisDOF.y * enemyDirection.y;
        float dotViewCone = 1 - viewCone / 180.0f;
        if (dot > dotViewCone)
        {
            inViewCone = true;
        }
        return inViewCone;
    }

    private static void DrawEllipse(Vector3 pos, float radius, Color color)
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
}
