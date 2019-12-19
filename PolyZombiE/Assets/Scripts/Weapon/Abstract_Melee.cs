using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Abstract_Melee : Abstract_Weapon {
    [SerializeField] float damage = 5.0f;
    [SerializeField] protected float primaryRange;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override public void PrimaryAttackDerived(LayerMask targetFilter, Abstract_Identity activator)
    {
        Vector2 from = transform.position;
        Vector2 direction = getDirectionVec();
        RaycastHit2D hit = Physics2D.Raycast(from, direction, primaryRange, targetFilter);
        
        Vector2 endPoint = from + direction.normalized * primaryRange;
        if (hit)
        {
            endPoint = hit.point;
            Abstract_Condition cCondition = hit.collider.GetComponent<Abstract_Condition>();
            float damageScaled = damage * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
            cCondition.subtractHealth(damageScaled, activator);
        }
        Debug.DrawLine(from, endPoint, Color.red, 5.0f);
    }

    override public void SecondaryAttackDerived(LayerMask targetFilter, Abstract_Identity activator) { }

    public float getPrimaryRange()
    {
        return primaryRange;
    }
    virtual public float getSecondaryRange() { return -1; }

}
