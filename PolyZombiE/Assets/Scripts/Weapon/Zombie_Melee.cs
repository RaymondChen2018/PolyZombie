using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Zombie_Melee : Abstract_Melee {
    [SerializeField] float biteDamage = 2.0f;
    [SerializeField] Zombie_Condition infectiousRef;
    [SerializeField] protected float biteRange = 5.0f;
    private float biteCoolDown = 1.0f;
    private float prevBiteTime = 0;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(infectiousRef);
	}

    override public void SecondaryAttack(LayerMask targetFilter)
    {
        if(Time.time > prevBiteTime + biteCoolDown)
        {
            prevBiteTime = Time.time;
            Vector2 from = transform.position;
            Vector2 direction = getDirectionVec();
            RaycastHit2D hit = Physics2D.Raycast(from, direction, biteRange, targetFilter);
            Vector2 endPoint = from + direction.normalized * primaryRange;
            if (hit)
            {
                endPoint = hit.point;
                Human_Condition cCondition = (Human_Condition)hit.collider.GetComponent<Abstract_Condition>();
                cCondition.addInfection(infectiousRef.GetInfectiousness());
                cCondition.subtractHealth(biteDamage);
            }
            Debug.DrawLine(from, endPoint, Color.yellow, 5.0f);
        }
    }

    public override float getSecondaryRange()
    {
        return biteRange;
    }
}
