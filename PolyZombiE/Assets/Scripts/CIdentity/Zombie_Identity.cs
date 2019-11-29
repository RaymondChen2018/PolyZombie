using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Zombie_Identity : Abstract_Identity
{
    public override Identity GetIdentity()
    {
        return Identity.Zombie;
    }

    public override void ChangeFaction(Identity newIdentity)
    {
        // Check
        Assert.IsTrue(newIdentity == Identity.Human);

        // Change Layer
        mainBody.layer = LayerMask.NameToLayer(CONSTANT.LAYER_NAME_HUMAN);

        // Call back
        healthAttribute.OnChangeFaction(Identity.Human);
        teamAttribute.OnChangeFaction(Identity.Human);
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
