using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nav_mesh : MonoBehaviour {
    [SerializeField] List<Transform> points = new List<Transform>();
    [SerializeField] List<Nav_mesh> neighboorMesh = new List<Nav_mesh>();
    [SerializeField] PolygonCollider2D polyCollider;

    [Header("Debug")]
    [SerializeField] bool debugOn = false;
    [SerializeField] Color debugColor = Color.red;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        if(polyCollider == null)
        {
            return;
        }


        // Clean null points
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i] == null)
            {
                points.RemoveAt(i);
                i--;
            }
        }

        // Adjust length if different
        Vector2[] newColliderPoints = new Vector2[points.Count];

        for (int i = 0; i < points.Count; i++)
        {
            newColliderPoints[i] = points[i].position;
        }

        polyCollider.points = newColliderPoints;

        // Debug
        if (debugOn)
        {
            Vector2[] newPolyColliderPoints = polyCollider.points;
            for (int i = 0; i < newPolyColliderPoints.Length - 1; i++)
            {
                Debug.DrawLine(newPolyColliderPoints[i], newPolyColliderPoints[i + 1], debugColor);
            }
            Debug.DrawLine(newPolyColliderPoints[0], newPolyColliderPoints[newPolyColliderPoints.Length - 1], debugColor);
        }
    }
}
