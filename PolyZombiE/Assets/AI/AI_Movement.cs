using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AI_Movement : MonoBehaviour {
    [Header("Navigation Stat")]
    private float navSize = 3.0f;
    [SerializeField] private LayerMask navBlockMask;
    [Tooltip("How often nav update for checks?")][SerializeField] float navRequestInterval = 3.0f;
    private float time_to_request = 0;
    [SerializeField] private CircleCollider2D colliderAgent;
    [SerializeField] Movement movement;

    List<Vector2> navPath = new List<Vector2>();
    //List<Node> nodePath = new List<Node>();
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
        navPath.Clear();
        navPath = path;
    }
    //public void setNavigationNodePath(List<Node> path)
    //{
    //    nodePath.Clear();
    //    nodePath = path;
    //}


    public void Move(Vector2 _targetPosition)
    {
        float agentSize = navSize;
        Vector2 targetPosition = _targetPosition;
        Vector2 thisPos = movement.getPosition();
        Vector2 direction = targetPosition - thisPos;
        float distance = direction.magnitude;
        float agentSizeTolerance = 0.2f;
        RaycastHit2D block = Physics2D.CircleCast(thisPos, agentSize, direction, distance, navBlockMask);

        if (block)
        {
            // Navigation unavaiable, move to target
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
                // Periodic update
                if(Time.time > time_to_request + navRequestInterval)
                {
                    time_to_request = Time.time;
                    Navigation_manual.RequestPath(new NavRequest(this, targetPosition, thisPos, agentSize));
                }

                // Debug
                if (debugOn)
                {
                    Debug.DrawLine(thisPos, navPath[0], debugColor);
                    for (int i = 0; i < navPath.Count - 1; i++)
                    {
                        Debug.DrawLine(navPath[i], navPath[i + 1], debugColor);
                    }
                }

                //// Get the node to go
                //Node curNode = nodePath[0];
                //Vector2 nodePos = curNode.transform.position;

                //Vector2 nodeDir = nodePos - thisPos;

                //// Get the location to slingshot toward
                //Vector2 slingToPos = targetPosition;
                //if(nodePath.Count > 1) // slerp to position is the nextNode
                //{
                //    slingToPos = nodePath[1].transform.position;
                //}

                //Vector2 straightPosition = slingToPos;

                //// Should perform slingshot?
                //Vector2 leftEdgeVec = Test_script.getPerpendicularEdgeVector(AgentEdge.left, thisPos, nodePos, agentSize);
                //Vector2 rightEdgeVec = Test_script.getPerpendicularEdgeVector(AgentEdge.right, thisPos, nodePos, agentSize);
                //Vector2 leftEdgePos = leftEdgeVec * agentSize + thisPos;
                //Vector2 rightEdgePos = rightEdgeVec * agentSize + thisPos;
                //Debug.DrawLine(nodePos, leftEdgePos, Color.white);
                //Debug.DrawLine(nodePos, rightEdgePos, Color.magenta);
                //bool leftEdgeAginstWall = Test_script.againstWall(leftEdgePos, curNode, slingToPos);
                //bool rightEdgeAginstWall = Test_script.againstWall(rightEdgePos, curNode, slingToPos);

                //DotTurn leftEdgeTurnDir = Test_script.turnLeft_or_Right(leftEdgePos, nodePos, slingToPos);
                //DotTurn rightEdgeTurnDir = Test_script.turnLeft_or_Right(rightEdgePos, nodePos, slingToPos);

                //Vector2 farEnd = Vector2.zero;

                //// Turn Left
                //if (leftEdgeAginstWall && leftEdgeTurnDir == DotTurn.TurnLeft)
                //{
                //    farEnd = (Vector2)(Quaternion.AngleAxis(-90, Vector3.forward) * (slingToPos - nodePos)).normalized * agentSize + nodePos;
                //    Debug.DrawLine(nodePos, farEnd);
                //    straightPosition = nodePos - leftEdgeVec * agentSize;
                //}
                //// Turn right
                //if (rightEdgeAginstWall && rightEdgeTurnDir == DotTurn.TurnRight)
                //{
                //    farEnd = (Vector2)(Quaternion.AngleAxis(90, Vector3.forward) * (slingToPos - nodePos)).normalized * agentSize + nodePos;
                //    Debug.DrawLine(nodePos, farEnd);
                //    straightPosition = nodePos - rightEdgeVec * agentSize;
                //}

                // Get node location and move there
                Vector2 straightPosition = navPath[0];
                //Debug.DrawLine(thisPos, straightPosition, Color.red);
                //Debug.DrawLine(thisPos, slingToPos, Color.cyan);
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
            Navigation_manual.CancelRequestPath(this);
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
