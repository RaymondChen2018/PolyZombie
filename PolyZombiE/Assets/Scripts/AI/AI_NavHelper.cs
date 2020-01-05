using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_NavHelper : MonoBehaviour {
    [SerializeField] private float navSize = 3.0f;
    [SerializeField] List<Vector2> navPath = new List<Vector2>();
    [SerializeField] Transform testDestine;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Navigation_manual.RequestPath(new NavRequest(this, transform.position, testDestine.position, navSize));

        if(navPath.Count > 0)
        {
            Debug.DrawLine(transform.position, navPath[navPath.Count - 1], Color.red);
            for (int i = 0; i < navPath.Count - 1; i++)
            {
                Debug.DrawLine(navPath[i], navPath[i+1], Color.red);
            }
            Debug.DrawLine(navPath[0], testDestine.position, Color.red);
        }
	}

    public float getNavSize()
    {
        return navSize;
    }

    public void setNavigationPath(List<Vector2> path)
    {
        navPath = path;
    }
}
