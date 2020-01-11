using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum DotTurn
{
    TurnRight,
    Straight,
    TurnLeft
}
public enum AgentEdge
{
    left = 1,
    right = -1,
}
public class Test_script : MonoBehaviour {
    [SerializeField] List<Transform> testPath = new List<Transform>();
    [SerializeField] float agentSize = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        if(testPath.Count <= 2)
        {
            return;
        }

        Vector2 thisPos = transform.position;
        Vector2 prevPos = thisPos;

        Vector2 segPoint0 = prevPos;
        Vector2 segPoint1 = testPath[0].position;
        Vector2 segPoint2 = testPath[1].position;

        Vector2[] verts = getSlerpVerts(segPoint0, segPoint1, segPoint2, agentSize, 3);

        // Original
        Debug.DrawLine(segPoint0, segPoint1, Color.gray);

        DotTurn turn= turnLeft_or_Right(segPoint0, segPoint1, segPoint2);
        if(turn == DotTurn.TurnRight)
        {
            Debug.DrawLine(segPoint1, segPoint2, Color.cyan);
        }
        else
        {
            Debug.DrawLine(segPoint1, segPoint2, Color.red);
        }

        // Verts
        //Debug.DrawLine(segPoint1, verts[0], Color.magenta);
        //Debug.DrawLine(segPoint1, verts[1], Color.cyan);
        //Debug.DrawLine(segPoint1, verts[2], Color.magenta);

        // Slerp
        //Debug.DrawLine(segPoint0, verts[0], Color.red);
        //Debug.DrawLine(verts[0], verts[1], Color.red);
        //Debug.DrawLine(verts[1], verts[2], Color.red);

        //prevPos = verts[2];
        //for (int i=0;i<testPath.Count - 2; i++)
        //{
        //    segPoint0 = testPath[i].position;
        //    segPoint1 = testPath[i+1].position;
        //    segPoint2 = testPath[i+2].position;

        //    verts = getSlerpVerts(segPoint0, segPoint1, segPoint2, agentSize, 3);

        //    // Original
        //    Debug.DrawLine(segPoint0, segPoint1, Color.gray);
        //    Debug.DrawLine(segPoint1, segPoint2, Color.gray);

        //    // Verts
        //    for (int j = 0; j < 3; j++)
        //    {
        //        Debug.DrawLine(segPoint1, verts[j], Color.magenta);
        //    }

        //    prevPos = verts[verts.Length - 1];
        //}
        
    }
    
    public static Vector2 getCloseNormal(Vector2 fromDir, Vector2 toDir)
    {
        float closeNormalAngle = Vector2.SignedAngle(fromDir, toDir) / 2.0f;
        if (closeNormalAngle > 0.0f)
        {
            return (Quaternion.AngleAxis(closeNormalAngle, Vector3.forward) * fromDir).normalized;
        }
        else
        {
            return (Quaternion.AngleAxis(-closeNormalAngle, Vector3.forward) * toDir).normalized;
        }
    }

    public static bool againstWall(Vector2 fromPos, Node node, Vector2 toPos)
    {
        Vector2 nodePos = node.transform.position;
        Vector2 fromV = fromPos - nodePos;
        Vector2 toV = toPos - nodePos;

        Vector2 closeNormal = getCloseNormal(fromV, toV);

        Vector2 leftWall = node.getLeftWall();
        Vector2 rightWall = node.getRightWall();

        return isWithinCloseAngle(closeNormal, leftWall, rightWall);
    }
    /// <summary>
    /// Is this vector between two vector?
    /// </summary>
    /// <param name="tested"></param>
    /// <param name="bound1"></param>
    /// <param name="bound2"></param>
    /// <returns></returns>
    public static bool isWithinCloseAngle(Vector2 tested, Vector2 leftBound, Vector2 rightBound)
    {
        return (Vector2.SignedAngle(leftBound, tested) > 0) 
            && Vector2.SignedAngle(tested, rightBound) > 0;
    }
    public static DotTurn turnLeft_or_Right(Vector2 pos0, Vector2 pos1, Vector2 pos2)
    {
        Vector2 segFirst =pos1 - pos0;
        Vector2 segSecond = pos2 - pos1;

        if (segFirst.y * segSecond.x > segFirst.x * segSecond.y)
        {
            return DotTurn.TurnRight;
        }
        else
        {
            return DotTurn.TurnLeft;
        }
    }


    public static Vector2 getPerpendicularEdgeVector(AgentEdge edge, Vector2 fromPos, Vector2 nodePos, float agentSize)
    {
        Vector2 dir = nodePos - fromPos;
        float distFromNode = dir.magnitude;
        float angleFromNegNodeDir = Mathf.Acos(agentSize / distFromNode) * 180.0f / Mathf.PI;
        return (Quaternion.AngleAxis((int)edge * angleFromNegNodeDir, Vector3.forward) * dir).normalized;
    }

    public static Vector2 getNearSlingShotPos(DotTurn turn, Vector2 fromPos, Vector2 nodePos, float agentSize)
    {
        Vector2 dir = nodePos - fromPos;
        float distFromNode = dir.magnitude;
        float angleFromNegNodeDir = Mathf.Acos(agentSize / distFromNode) * 180.0f / Mathf.PI;

        if (turn == DotTurn.TurnRight)// turn right
        {
            return (Vector2)(Quaternion.AngleAxis(angleFromNegNodeDir, Vector3.forward) * -dir).normalized * agentSize + nodePos;
        }
        else// turn left
        {
            return (Vector2)(Quaternion.AngleAxis(-angleFromNegNodeDir, Vector3.forward) * -dir).normalized * agentSize + nodePos;
        }
    }

    public static Vector2[] getSlerpVerts(Vector2 segPoint0, Vector2 segPoint1, Vector2 segPoint2, float agentSize, int slerpVertCount)
    {
        
        Vector2[] ret = new Vector2[slerpVertCount];

        //Debug.DrawLine(segPoint0, segPoint1, Color.red);
        //Debug.DrawLine(segPoint1, segPoint2, Color.red);

        Vector2 seg0 = segPoint1 - segPoint0;
        Vector2 seg1 = segPoint2 - segPoint1;

        // Draw normal
        float turnAngle = Vector2.SignedAngle(seg0, seg1);
        //float openAngle = 180 + Mathf.Abs(turnAngle);

        int additionalVert = slerpVertCount - 2;
        int cuts = additionalVert + 1;
        if (turnAngle > 0.0f)// turn right
        {
            Vector2 vecNear = (Quaternion.AngleAxis(90, Vector3.forward) * -seg0).normalized;
            Vector2 vecFar = (Quaternion.AngleAxis(-90, Vector3.forward) * seg1).normalized;
            ret[0] = vecNear * agentSize + segPoint1;
            ret[slerpVertCount - 1] = vecFar * agentSize + segPoint1;

            float openAngle = Vector2.Angle(vecNear, vecFar);
            for (int i = 1; i < slerpVertCount - 1; i++)
            {
                float stepAngle = (openAngle / cuts) * i;
                Vector2 vecSlerp = (Quaternion.AngleAxis(stepAngle, Vector3.forward) * vecNear).normalized;
                ret[i] = segPoint1 + vecSlerp * agentSize;
            }

            //Debug.DrawLine(segPoint1, segPoint1 + vecFar);
            
        }
        else// turn left
        {
            Vector2 vecNear = (Quaternion.AngleAxis(-90, Vector3.forward) * -seg0).normalized;
            Vector2 vecFar = (Quaternion.AngleAxis(90, Vector3.forward) * seg1).normalized;
            ret[0] = vecNear * agentSize + segPoint1;
            ret[slerpVertCount - 1] = vecFar * agentSize + segPoint1;
            float openAngle = Vector2.Angle(vecNear, vecFar);
            for (int i = 1; i < slerpVertCount - 1; i++)
            {
                float stepAngle = (openAngle / cuts) * i;
                Vector2 vecSlerp = (Quaternion.AngleAxis(-stepAngle, Vector3.forward) * vecNear).normalized;
                ret[i] = segPoint1 + vecSlerp * agentSize;
            }
            //Debug.DrawLine(segPoint1, segPoint1 + vecFar);

        }
        //Vector2 concaveNormalHandle = concaveNorm * agentSize + segPoint1;
        //ret[1] = concaveNormalHandle;

        //// Perpen
        //float smallAngle = sideAngleFromNorm - 90.0f;
        //Vector2 leftNorm = (Quaternion.AngleAxis(smallAngle, Vector3.forward) * concaveNorm).normalized;
        //Vector2 rightNorm = (Quaternion.AngleAxis(-smallAngle, Vector3.forward) * concaveNorm).normalized;
        //Vector2 leftNormalHandle = leftNorm * agentSize + segPoint1;
        //Vector2 rightNormalHandle = rightNorm * agentSize + segPoint1;

        //if(turnAngle > 0.0f) // right vert first
        //{
        //    ret[0] = rightNormalHandle;
        //    ret[2] = leftNormalHandle;
        //}
        //else
        //{
        //    ret[0] = leftNormalHandle;
        //    ret[2] = rightNormalHandle;
        //}

        return ret;
    }
}
