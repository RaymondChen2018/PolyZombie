using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Human_Identity : Abstract_Identity
{
    public override Identity GetIdentity()
    {
        return Identity.Human;
    }

    public override void ChangeFaction(Identity newIdentity)
    {
        // Check
        Assert.IsTrue(newIdentity == Identity.Zombie);

        // Change Layer
        mainBody.layer = LayerMask.NameToLayer(CONSTANT.LAYER_NAME_ZOMBIE);

        // Call back
        healthAttribute.OnChangeFaction(Identity.Zombie);
        teamAttribute.OnChangeFaction(Identity.Zombie);
        
    }

    public override void Die()
    {
        // Call back
        teamAttribute.OnDeath();
        healthAttribute.OnDeath();

        // Destroy this object
        Destroy(gameObject);
    }
}
