using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Abstract_Melee : Abstract_Weapon {
    [SerializeField] float damage = 5.0f;
    [SerializeField] protected float primaryRange = 5.0f;
    private float primaryCoolDown = 1.0f;
    private float prevPrimaryTime = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override public void PrimaryAttack(LayerMask targetFilter)
    {
        if(Time.time > prevPrimaryTime + primaryCoolDown)
        {
            prevPrimaryTime = Time.time;
            Vector2 from = transform.position;
            Vector2 direction = getDirectionVec();
            RaycastHit2D hit = Physics2D.Raycast(from, direction, primaryRange, targetFilter);
            Vector2 endPoint = from + direction.normalized * primaryRange;
            if (hit)
            {
                endPoint = hit.point;
                Abstract_Condition cCondition = hit.collider.GetComponent<Abstract_Condition>();
                cCondition.subtractHealth(damage);
            }
            Debug.DrawLine(from, endPoint, Color.red, 5.0f);
        }
    }

    override public void SecondaryAttack(LayerMask targetFilter) { }

    public override float getPrimaryRange()
    {
        return primaryRange;
    }
    public override float getSecondaryRange() { return -1; }

}
