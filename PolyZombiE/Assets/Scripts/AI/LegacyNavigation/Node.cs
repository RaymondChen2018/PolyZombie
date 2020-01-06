using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Node : MonoBehaviour {
    public int objective = 0;
    public int area_code = 0;
    public bool isDynamic = false;
    public List<GameObject> neighboor;
    public Navigation_manual._Node reference;

    [SerializeField] private float normalAngle = 0.0f;
    private Vector2 normalEndPosition;


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
        Handles.Label(transform.position, gameObject.name);

        float radian = normalAngle * Mathf.PI / 180.0f;
        normalEndPosition = (Vector2)transform.position + new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized * 2.0f;
        Debug.DrawLine(transform.position, normalEndPosition);
    }

    public Vector2 getNormal()
    {
        return (normalEndPosition - (Vector2)transform.position).normalized;
    }
    public Vector2 getAgentScaledPosition(float agentSize)
    {
        Vector2 normal = (normalEndPosition - (Vector2)transform.position).normalized;
        return (Vector2)transform.position + normal * agentSize;
    }
}
