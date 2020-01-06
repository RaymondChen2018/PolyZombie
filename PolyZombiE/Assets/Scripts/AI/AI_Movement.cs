using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AI_Movement : MonoBehaviour {
    [Header("Navigation Stat")]
    private float navSize = 3.0f;
    [SerializeField] private LayerMask navBlockMask;
    [SerializeField] private CircleCollider2D colliderAgent;
    [SerializeField] Movement movement;

    List<Vector2> navPath = new List<Vector2>();
    private Vector2 prevTargetPosition;

    [Header("Debug")]
    [SerializeField] private bool debugOn = false;
    [SerializeField] private Color debugColor = Color.red;
    

	// Use this for initialization
	void Start () {
        Assert.IsNotNull(movement);
        Assert.IsNotNull(colliderAgent);
        navSize = colliderAgent.radius;
	}	

    public float getNavSize()
    {
        return navSize;
    }

    public void setNavigationPath(List<Vector2> path)
    {
        navPath = path;
    }

    public void Move(Vector2 _targetPosition)
    {
        float agentSize = navSize;
        Vector2 targetPosition = _targetPosition;
        Vector2 thisPos = movement.getPosition();
        Vector2 direction = targetPosition - thisPos;
        float distance = direction.magnitude;
        float agentSizeTolerance = 0.2f;
        RaycastHit2D block = Physics2D.CircleCast(thisPos, agentSize - agentSizeTolerance, direction, distance, navBlockMask);

        if (block)
        {
            // Navigation unavaiable
            if (navPath.Count == 0)
            {
                Navigation_manual.RequestPath(new NavRequest(this, targetPosition, thisPos, agentSize));
                movement.Move(targetPosition - thisPos);
                Debug.LogWarning("Navigation unavailable");
            }
            // Destination changed
            else if (prevTargetPosition != targetPosition)
            {
                Navigation_manual.RequestPath(new NavRequest(this, targetPosition, thisPos, agentSize));
                movement.Move(targetPosition - thisPos);
            }
            // Navigation available
            else
            {
                // Debug
                if (debugOn)
                {
                    Debug.DrawLine(thisPos, navPath[0], debugColor);
                    for (int i = 0; i < navPath.Count - 1; i++)
                    {
                        Debug.DrawLine(navPath[i], navPath[i + 1], debugColor);
                    }
                }

                // Get node location and move there
                Vector2 straightPosition = navPath[0];
                movement.Move(straightPosition - thisPos);

                // Reach node location
                if (movement.positionReached(straightPosition, agentSizeTolerance))
                {
                    navPath.RemoveAt(0);
                }
            }
        }
        else
        {
            if (debugOn)
            {
                Debug.DrawLine(thisPos, targetPosition, debugColor);
            }
            movement.Move(targetPosition - thisPos);
        }

        prevTargetPosition = targetPosition;
    }

    public float getNavigationDistance(Vector2 from, Vector2 to)
    {
        float ret = 0;

        List<Vector2> route = Navigation_manual.RequestPathInstant(from, to, navSize);
        if(route.Count > 0)
        {
            ret += (route[0] - movement.getPosition()).magnitude;
            for(int i = 1; i < route.Count; i++)
            {
                ret += (route[i] - route[i-1]).magnitude;
            }
        }
        else
        {
            ret += (to - movement.getPosition()).magnitude;
        }

        return ret;
    }
}
