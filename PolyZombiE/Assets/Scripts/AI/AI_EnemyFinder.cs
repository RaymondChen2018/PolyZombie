using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class AI_EnemyFinder : MonoBehaviour {
    [SerializeField] private float viewDist = 10.0f;
    [SerializeField] private float viewConeAngle = 60.0f;
    [SerializeField] private LayerMask losObstacleLayerMask;
    [SerializeField] private Movement movement;
    [SerializeField] private Orient orient;
    [SerializeField] private Team_Attribute teamAttribute;
    [SerializeField] private Animator AI_StateMachine;
    [SerializeField] private List<Collider2D> enemyInSight;
    [SerializeField] private List<Collider2D> enemyInMemory;
    private Transform closestEnemy = null;

    [Tooltip("AI established LOS to enemy (old & new enemies)")] [SerializeField] UnityEvent OnSeeEnemyOnce = new UnityEvent();

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(movement);
        Assert.IsNotNull(orient);
        Assert.IsNotNull(teamAttribute);

        enemyInSight = new List<Collider2D>();
        enemyInMemory = new List<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        // Range detection
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(movement.getPosition(), viewDist, teamAttribute.GetOpponentLayerMask());
        DrawEllipse(transform.position, viewDist, Color.yellow);

        // View Cone detection
        enemyInSight.Clear();
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
                enemyInSight.Add(enemyCollider);
                Func_OnSeeEnemy(enemyCollider);
            }
        }

        // Clean memory
        for (int i = 0; i < enemyInMemory.Count; i++)
        {
            if(enemyInMemory[i] == null)
            {
                enemyInMemory.RemoveAt(i);
                i--;
            }
        }

        // State machine
        AI_StateMachine.SetInteger("EnemyInSight", enemyInSight.Count);
        AI_StateMachine.SetInteger("EnemyRemembered", enemyInMemory.Count);

        // Find closest enemy
        if (enemyInMemory.Count > 0)
        {
            Transform tmp = enemyInMemory[0].transform;
            Transform ret = tmp;
            float closestDistSqrTmp = ((Vector2)tmp.position - movement.getPosition()).sqrMagnitude;
            float closestDistSqr = closestDistSqrTmp;
            for (int i = 1; i < enemyInMemory.Count; i++)
            {
                tmp = enemyInMemory[i].transform;
                closestDistSqrTmp = ((Vector2)tmp.position - movement.getPosition()).sqrMagnitude;
                if (closestDistSqr > closestDistSqrTmp)
                {
                    closestDistSqr = closestDistSqrTmp;
                    ret = tmp;
                }
            }
            closestEnemy = ret;
        }
        else
        {
            closestEnemy = null;
        }
    }

    private void Func_OnSeeEnemy(Collider2D enemyCollider)
    {
        // Call back
        OnSeeEnemyOnce.Invoke();
        OnSeeEnemyOnce = new UnityEvent();

        // Add to memory if new
        if (!enemyInMemory.Contains(enemyCollider))
        {
            enemyInMemory.Add(enemyCollider);
        }
    }

    public Transform getClosestEnemy()
    {
        return closestEnemy;
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
