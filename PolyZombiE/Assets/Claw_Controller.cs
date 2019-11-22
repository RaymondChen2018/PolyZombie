using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw_Controller : MonoBehaviour {
    [SerializeField] float damage = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AttackRay(Vector2 from, Vector2 to, LayerMask targetFilter)
    {
        Vector2 dir = to - from;
        float dist = dir.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(from, dir, dist, targetFilter);

    }

    public void AttackBox(Vector2 from, Vector2 to, float size, LayerMask targetFilter)
    {
        Vector2 dir = to - from;
        float dist = dir.magnitude;
        //RaycastHit2D hit = Physics2D.BoxCast(from, dir, dist, targetFilter);
    }
}
