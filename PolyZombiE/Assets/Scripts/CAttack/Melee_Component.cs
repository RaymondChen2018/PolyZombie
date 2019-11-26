using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Component : MonoBehaviour {
    [SerializeField] float damage = 5.0f;
    [SerializeField] float reach = 5.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AttackRay(Vector2 from, Vector2 direction, LayerMask targetFilter)
    {
        RaycastHit2D hit = Physics2D.Raycast(from, direction, reach, targetFilter);
        Vector2 endPoint = from + direction.normalized * reach;
        if (hit)
        {
            endPoint = hit.point;
        }
        Debug.DrawLine(from, endPoint, Color.red, 5.0f);
    }

    //public void AttackBox(Vector2 from, Vector2 to, float size, LayerMask targetFilter)
    //{
    //    Vector2 dir = to - from;
    //    float dist = dir.magnitude;
    //    //RaycastHit2D hit = Physics2D.BoxCast(from, dir, dist, targetFilter);
    //}
}
