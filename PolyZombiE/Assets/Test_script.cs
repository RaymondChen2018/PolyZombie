using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


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
        Debug.DrawLine(segPoint1, segPoint2, Color.gray);

        // Verts
        Debug.DrawLine(segPoint1, verts[0], Color.magenta);
        Debug.DrawLine(segPoint1, verts[1], Color.cyan);
        Debug.DrawLine(segPoint1, verts[2], Color.magenta);

        // Slerp
        Debug.DrawLine(segPoint0, verts[0], Color.red);
        Debug.DrawLine(verts[0], verts[1], Color.red);
        Debug.DrawLine(verts[1], verts[2], Color.red);

        prevPos = verts[2];
        for (int i=0;i<testPath.Count - 2; i++)
        {
            segPoint0 = testPath[i].position;
            segPoint1 = testPath[i+1].position;
            segPoint2 = testPath[i+2].position;

            verts = getSlerpVerts(segPoint0, segPoint1, segPoint2, agentSize, 3);

            // Original
            Debug.DrawLine(segPoint0, segPoint1, Color.gray);
            Debug.DrawLine(segPoint1, segPoint2, Color.gray);

            // Verts
            for (int j = 0; j < 3; j++)
            {
                Debug.DrawLine(segPoint1, verts[j], Color.magenta);
            }

            prevPos = verts[verts.Length - 1];
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
