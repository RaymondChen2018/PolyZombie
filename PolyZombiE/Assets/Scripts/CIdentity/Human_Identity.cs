using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Human_Identity : Abstract_Identity
{
    [SerializeField] private GameObject zombieVersion;

    public override Identity GetIdentity()
    {
        return Identity.Human;
    }

    public void TurnZombie()
    {
        // Change Layer
        mainBody.layer = LayerMask.NameToLayer(CONSTANT.LAYER_NAME_ZOMBIE);

        // Call back
        healthAttribute.OnChangeFaction(Identity.Zombie);
        teamAttribute.OnChangeFaction(Identity.Zombie);

        // Alter components
        Assert.IsNotNull(zombieVersion);
        Instantiate(zombieVersion, transform.position,transform.rotation);
        Destroy(gameObject);

        //Debug.LogError("Infection implementation not completed");

        //Zombie_Condition zombieCondition = healthAttribute.gameObject.AddComponent<Zombie_Condition>();
        //Destroy(healthAttribute);
        //healthAttribute = zombieCondition;

        //Zombie_Movement zombieMovement = movementAttribute.gameObject.AddComponent<Zombie_Movement>();
        //Destroy(movementAttribute);
        //movementAttribute = zombieMovement;

        //Zombie_Melee zombieMelee = meleeAttribute.gameObject.AddComponent<Zombie_Melee>();
        //Destroy(meleeAttribute);
        //meleeAttribute = zombieMelee;
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
