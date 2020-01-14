using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DealDamage : MonoBehaviour, IWeapon_effector
{
    public void inflict(AttackInfo attackInfo)
    {
        if (attackInfo.activator == null)
        {
            attackInfo.activator = World_Identity.singleton;
        }

        Assert.IsNotNull(attackInfo.activator);
        Assert.IsNotNull(attackInfo.victim);

        Abstract_Identity activator = attackInfo.activator;
        Abstract_Identity victim = attackInfo.victim.GetComponent<Abstract_Identity>();

        // Hit character
        if (victim != null)
        {
            victim.getHealthComponent().subtractHealth(new DamageInfo(attackInfo.damage, activator.transform.position - victim.transform.position, activator));
        }
        else
        {

        }
    }
}
