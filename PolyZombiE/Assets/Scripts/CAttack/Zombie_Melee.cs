using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Zombie_Melee : Abstract_Melee {
    [SerializeField] float biteDamage = 2.0f;
    [SerializeField] Zombie_Condition infectiousRef;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(infectiousRef);
	}

    public void BiteRay(Vector2 from, Vector2 direction, LayerMask targetFilter)
    {
        RaycastHit2D hit = Physics2D.Raycast(from, direction, reach, targetFilter);
        Vector2 endPoint = from + direction.normalized * reach;
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
