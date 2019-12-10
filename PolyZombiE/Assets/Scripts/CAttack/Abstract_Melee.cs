using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Abstract_Melee : MonoBehaviour {
    [SerializeField] float damage = 5.0f;
    [SerializeField] protected float reach = 5.0f;

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
            Abstract_Condition cCondition = hit.collider.GetComponent<Abstract_Condition>();
            cCondition.subtractHealth(damage);
        }
        Debug.DrawLine(from, endPoint, Color.red, 5.0f);
    }
}
