using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Zombie_Melee : Abstract_Melee {
    [SerializeField] float biteDamage = 2.0f;
    [SerializeField] protected float biteRange;

    // Use this for initialization
    void Start () {

	}

    override public void SecondaryAttackDerived(LayerMask targetFilter, Abstract_Identity activator)
    {
        Zombie_Identity activatorZomb = activator.GetComponent<Zombie_Identity>();
        Assert.IsNotNull(activatorZomb);

        Collider2D[] colliders = new Collider2D[hitMultiple];
        Collider2D meleeBox = POI.GetComponent<Collider2D>();
        Assert.IsNotNull(meleeBox);
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(targetFilter);
        int hitCount = Physics2D.OverlapCollider(meleeBox, contactFilter, colliders);

        for (int i = 0; i < hitCount; i++)
        {
            // Victim must be human
            Human_Condition cCondition = colliders[i].GetComponent<Human_Condition>();
            Assert.IsNotNull(cCondition);

            // Infect
            float infectiousness = activatorZomb.GetInfectiousness();
            cCondition.addInfection(infectiousness, activatorZomb);

            // Damage
            float biteDamageScaled = biteDamage * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
            cCondition.subtractHealth(biteDamageScaled, activator);
        }
    }

    public override float getSecondaryRange()
    {
        return biteRange;
    }
}
