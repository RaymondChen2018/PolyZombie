using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AI_EnemyFinder : MonoBehaviour {
    [SerializeField] private float viewDist = 10.0f;
    [SerializeField] private float viewConeAngle = 60.0f;
    [SerializeField] private Movement movement;
    [SerializeField] private Orient orient;
    [SerializeField] private Team_Attribute teamAttribute;
    [SerializeField] private Animator AI_StateMachine;
    private Collider2D[] enemyList;
    private Transform closestEnemy = null;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(movement);
        Assert.IsNotNull(orient);
        Assert.IsNotNull(teamAttribute);
    }
	
	// Update is called once per frame
	void Update () {
        enemyList = Physics2D.OverlapCircleAll(movement.getPosition(), viewDist, teamAttribute.GetOpponentLayerMask());
        if(enemyList.Length > 0)
        {
            Transform tmp = enemyList[0].transform;
            Transform ret = tmp;
            float closestDistSqrTmp = ((Vector2)tmp.position - movement.getPosition()).sqrMagnitude;
            float closestDistSqr = closestDistSqrTmp;
            for (int i = 1; i < enemyList.Length; i++)
            {
                tmp = enemyList[i].transform;
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
        AI_StateMachine.SetInteger("EnemyInSight", enemyList.Length);

        DrawEllipse(transform.position, viewDist, Color.yellow);
    }

    public Transform getClosestEnemy()
    {
        return closestEnemy;
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
