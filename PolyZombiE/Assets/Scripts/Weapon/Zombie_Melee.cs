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

    override public void SecondaryAttack(LayerMask targetFilter, Abstract_Identity activator)
    {
        Zombie_Identity activatorZomb = activator.GetComponent<Zombie_Identity>();
        Assert.IsNotNull(activatorZomb);

        prevSecondaryTime = Time.time;
        Vector2 from = transform.position;
        Vector2 direction = getDirectionVec();
        RaycastHit2D hit = Physics2D.Raycast(from, direction, biteRange, targetFilter);
        Vector2 endPoint = from + direction.normalized * biteRange;
        if (hit)
        {
            endPoint = hit.point;

            // infect & damage
            Human_Condition cCondition = hit.collider.GetComponent<Human_Condition>();
            Assert.IsNotNull(cCondition);
            cCondition.addInfection(activatorZomb.GetInfectiousness(), activatorZomb);
            cCondition.subtractHealth(biteDamage, activator);
        }
        Debug.DrawLine(from, endPoint, Color.yellow, 5.0f);
    }

    public override float getSecondaryRange()
    {
        return biteRange;
    }
}
