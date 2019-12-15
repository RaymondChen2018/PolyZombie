using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Zombie_Identity : Abstract_Identity
{
    /// <summary>
    /// Zombie with higher infected(ious)ness can infect human faster.
    /// </summary>
    [SerializeField] private float infectiousness = CONSTANT.MINIMUM_INFECTIOUSNESS;

    [SerializeField] private UnityEvent OnInfectedSomeOne = new UnityEvent();

    public override Identity GetIdentity()
    {
        return Identity.Zombie;
    }

    public override void Die()
    {
        // Call back
        teamAttribute.Func_OnDeath();
        healthAttribute.Func_OnDeath();

        // Destroy this object
        Destroy(gameObject);
    }


    public void Func_OnInfectedSomeOne()
    {
        OnInfectedSomeOne.Invoke();
    }

    public float GetInfectiousness()
    {
        return infectiousness;
    }
}
