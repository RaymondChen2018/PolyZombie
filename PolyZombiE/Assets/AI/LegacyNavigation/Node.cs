using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;


public class Node : MonoBehaviour {
    public int objective = 0;
    public int area_code = 0;
    public bool isDynamic = false;
    public List<GameObject> neighboor;
    public Navigation_manual._Node reference;

    [SerializeField] private float normalAngle = 0.0f;
    [SerializeField] private float sideAngle = 0.0f;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < neighboor.Count; i++)
        {
            if (neighboor[i] != null)
            {
                if (neighboor[i].GetComponent<Node>().neighboor.Contains(gameObject))
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawLine(transform.position, neighboor[i].transform.position);
            }
        }
        //Handles.Label(transform.position, gameObject.name);

        Debug.DrawLine(transform.position, getScaledNormalPosition(2.0f));
    }

    public Vector2 getScaledNormalPosition(float agentSize)
    {
        float radian = normalAngle * Mathf.PI / 180.0f;
        
        return (Vector2)transform.position + new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized * agentSize;
    }

    public Vector2 getLeftWall()
    {
        float radian = (normalAngle + sideAngle) * Mathf.PI / 180.0f;
        return (new Vector2(Mathf.Cos(radian), Mathf.Sin(radian))).normalized;
    }
    public Vector2 getRightWall()
    {
        float radian = (normalAngle - sideAngle) * Mathf.PI / 180.0f;
        return (new Vector2(Mathf.Cos(radian), Mathf.Sin(radian))).normalized;
    }

    public Vector2 getLeftNormalPosition(float agentSize)
    {
        float angleDegree = normalAngle + sideAngle - 90.0f;
        float radian = angleDegree * Mathf.PI / 180.0f;

        return (Vector2)transform.position + new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized * agentSize;
    }

    public Vector2 getRightNormalPosition(float agentSize)
    {
        float angleDegree = normalAngle - sideAngle + 90.0f;
        float radian = angleDegree * Mathf.PI / 180.0f;

        return (Vector2)transform.position + new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized * agentSize;
    }

    public Vector2 getNormVec()
    {
        return new Vector2(Mathf.Cos(normalAngle * Mathf.PI / 180.0f), Mathf.Sin(normalAngle * Mathf.PI / 180.0f)).normalized;
    }

    public Vector2 getTangentVector(float agentRadius, Vector2 agentPos)
    {
        if(agentRadius <= 0.0f)
        {
            return Vector2.zero;
        }

        Vector2 normVector = getNormVec();
        Vector2 agentForward = agentPos - (Vector2)transform.position;
        float agentDistance = agentForward.magnitude;
        float big = Vector2.SignedAngle(normVector, agentForward);

        float corner = Mathf.Acos(agentRadius / agentDistance) * 180.0f / Mathf.PI;
        float tangentAngle = Mathf.Abs(big) - corner;
        if(big <= 0.0f)
        {
            tangentAngle *= -1;
        }
        Vector2 tangentVec = Quaternion.AngleAxis(tangentAngle, Vector3.forward) * normVector;
        return tangentVec.normalized;
    }
    public Vector2 getTangentVectorNextNode(float agentRadius, Node nextNode)
    {
        if (agentRadius <= 0.0f)
        {
            return Vector2.zero;
        }

        Vector2 thisNorm = getNormVec();
        Vector2 nextNorm = nextNode.getNormVec();
        Vector2 nextForward = nextNode.transform.position - transform.position;
        float dist = nextForward.magnitude;
        if(Vector2.Dot(thisNorm, nextNorm) < -0.5f) // Diagonal
        {
            float big = Mathf.Acos(agentRadius / dist) * 180.0f / Mathf.PI;
            Vector2 thisTangentVec = Quaternion.AngleAxis(big, Vector3.forward) * nextForward;
            return thisTangentVec.normalized;
        }
        else
        {
            Vector2 thisTangentVec = Quaternion.AngleAxis(90.0f, Vector3.forward) * nextForward;
            return thisTangentVec.normalized;
        }
    }



    public void setNormalAngle(float _angle)
    {
        normalAngle = _angle;
    }

    public void setSideAngle(float _angle)
    {
        sideAngle = _angle;
    }

    public float getNormalAngle()
    {
        return normalAngle;
    }

    public float getSideAngle()
    {
        return sideAngle;
    }
}
