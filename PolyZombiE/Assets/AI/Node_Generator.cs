using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Node_Generator : MonoBehaviour {
    [SerializeField] private GameObject NodePrefab;
    [SerializeField] private float distAgainstCorner = 0.1f;
    [SerializeField] private LayerMask wallMask;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        
    }

    public void buildNodeGraph()
    {
        // Verify prefab
        Assert.IsNotNull(NodePrefab);

        // Clean children
        if (transform.childCount > 0)
        {
            Debug.LogWarning("Please clean children nodes first!");
        }

        // Biild
        int wallLayer = LayerMask.NameToLayer("Wall");
        int lowwallLayer = LayerMask.NameToLayer("LowWall");

        PolygonCollider2D[] polys = FindObjectsOfType<PolygonCollider2D>();
        for(int i = 0; i < polys.Length; i++)
        {
            if (polys[i].gameObject.layer == wallLayer || polys[i].gameObject.layer == lowwallLayer)
            {
                generateNodesPoly(polys[i]);
            }
            
        }
        BoxCollider2D[] boxes = FindObjectsOfType<BoxCollider2D>();
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].gameObject.layer == wallLayer || boxes[i].gameObject.layer == lowwallLayer)
            {
                generateNodesCube(boxes[i]);
            }
        }
    }

    Matrix4x4 rotScaleOffset;
    Vector2 positionOffset;
    Vector2 transformPoint(Vector2 point)
    {
        return (Vector2)(rotScaleOffset * point) + positionOffset;
    }

    private void generateNodesCube(BoxCollider2D box)
    {

        //rotScaleOffset = Matrix4x4.identity;
        //positionOffset = Vector2.zero;
        rotScaleOffset = box.transform.localToWorldMatrix;
        positionOffset = box.transform.position;
        Vector2[] points = new Vector2[4];
        Vector2 max = box.bounds.max;
        Vector2 min = box.bounds.min;
        points[0] = new Vector2(box.size.x / 2.0f, box.size.y / 2.0f);
        points[1] = new Vector2(box.size.x / -2.0f, box.size.y / 2.0f);
        points[2] = new Vector2(box.size.x / -2.0f, box.size.y / -2.0f);
        points[3] = new Vector2(box.size.x / 2.0f, box.size.y / -2.0f);
        generateNodesPath(points);
    }

    private void generateNodesPoly(PolygonCollider2D poly)
    {
        for (int i = 0; i < poly.pathCount; i++)
        {
            rotScaleOffset = poly.transform.localToWorldMatrix;
            positionOffset = poly.transform.position;
            generateNodesPath(poly.GetPath(i));
        }
    }

    private void generateNodesPath(Vector2[] points)
    {
        Assert.IsTrue(points.Length >= 3);
        int count = points.Length;
        //float ratio = 1.0f / count;

        analyze(points[1], points[0], points[count - 1]);
        analyze(points[0], points[count - 1], points[count - 2]);
        for (int i = 1; i < count - 1; i++)
        {
            analyze(points[i + 1], points[i], points[i - 1]);
        }
    }

    private void analyze(Vector2 leftPoint, Vector2 thisPoint, Vector2 rightPoint)
    {
        Vector2 leftPointT = transformPoint(leftPoint);
        Vector2 thisPointT = transformPoint(thisPoint);
        Vector2 rightPointT = transformPoint(rightPoint);

        Vector2 left = leftPointT - thisPointT;
        Vector2 right = rightPointT - thisPointT;

        float angle = Vector2.SignedAngle(left, right);
        if (angle > 0)
        {
            float angleFromRight = (360.0f - angle) / 2.0f;
            Vector2 normal = (Quaternion.AngleAxis(angleFromRight, Vector3.forward) * right).normalized;

            spawnNode(thisPointT, normal, angleFromRight);
        }
    }

    private void spawnNode(Vector2 corner, Vector2 normal, float angle)
    {
        Vector2 cornerTransformed = corner;//(Vector2)(rotScaleOffset * corner) + positionOffset;
        Vector2 normalTransformed = normal;//(rotScaleOffset * normal).normalized;
        Vector2 spawnPos = cornerTransformed + normalTransformed * distAgainstCorner;
        if(!Physics2D.OverlapPoint(spawnPos, wallMask))
        {
            //Debug.DrawLine(cornerTransformed, spawnPos);

            Node node = Instantiate(NodePrefab, spawnPos, Quaternion.identity, transform).GetComponent<Node>();
            node.setNormalAngle(Mathf.Atan2(normal.y, normal.x) * 180.0f / Mathf.PI);
            node.setSideAngle(angle);
        }
    }

    public void connectNodes()
    {
        int children = transform.childCount;
        for (int i = 0; i < children; i++)
        {
            for(int j = i+1; j < children; j++)
            {
                Node a = transform.GetChild(i).GetComponent<Node>();
                Node b = transform.GetChild(j).GetComponent<Node>();

                if(analyzeNode(a, b) && analyzeNode(b, a))
                {
                    connectTwoNodes(a, b);
                }
            }
        }
    }
    private void connectTwoNodes(Node a, Node b)
    {
        if (!a.neighboor.Contains(b.gameObject))
        {
            a.neighboor.Add(b.gameObject);
        }

        if (!b.neighboor.Contains(a.gameObject))
        {
            b.neighboor.Add(a.gameObject);
        }
    }

    /// <summary>
    ///  Is b a valid node to connect to relative to a?
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private bool analyzeNode(Node a, Node b)
    {
        // Test raycast
        bool blocked = (Physics2D.Linecast(a.transform.position, b.transform.position, wallMask).collider != null);

        // Test angle
        float normalRadian = a.getNormalAngle() * Mathf.PI / 180.0f;
        Vector2 normalVec = new Vector2(Mathf.Cos(normalRadian), Mathf.Sin(normalRadian));
        Vector2 targetDir = b.transform.position - a.transform.position;
        float complementSideAngle = 180.0f - a.getSideAngle();
        bool angleValid = Vector2.Angle(normalVec, targetDir) > complementSideAngle;

        return !blocked && angleValid;
    }

    public void cleanNodes()
    {
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
    }
}
