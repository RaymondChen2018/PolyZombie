using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Abstract_Melee : Abstract_Weapon {
    [SerializeField] float damage = 5.0f;
    [SerializeField] protected float primaryRange;
    [SerializeField] protected int hitMultiple = 1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override public void PrimaryAttackDerived(LayerMask targetFilter, Abstract_Identity activator)
    {
        Collider2D[] colliders = new Collider2D[hitMultiple];
        Collider2D meleeBox = POI.GetComponent<Collider2D>();
        Assert.IsNotNull(meleeBox);
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(targetFilter);
        int hitCount = Physics2D.OverlapCollider(meleeBox, contactFilter, colliders);

        for(int i = 0; i < hitCount; i++)
        {
            Abstract_Condition cCondition = colliders[i].GetComponent<Abstract_Condition>();
            float damageScaled = damage * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
            cCondition.subtractHealth(damageScaled, activator);

            //meleeThisPrimary(cCondition, activator);
        }
    }

    override public void SecondaryAttackDerived(LayerMask targetFilter, Abstract_Identity activator) { }


    //virtual protected void meleeThisPrimary(Abstract_Condition victim, Abstract_Identity activator)
    //{
    //    float damageScaled = damage * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
    //    victim.subtractHealth(damageScaled, activator);
    //}
    //virtual protected void meleeThisSecondary(Abstract_Condition victim, Abstract_Identity activator)
    //{
    //    float damageScaled = damage * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
    //    victim.subtractHealth(damageScaled, activator);
    //}


    public float getPrimaryRange()
    {
        return primaryRange;
    }
    virtual public float getSecondaryRange() { return -1; }
}
