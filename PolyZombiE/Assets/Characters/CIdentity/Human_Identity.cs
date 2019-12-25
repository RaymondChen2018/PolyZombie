using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Human_Identity : Abstract_Identity
{
    [Header("Human")]
    [Header("Infected Version")]
    [SerializeField] private GameObject zombieVersion;

    public override IDENTITY GetIdentity()
    {
        return IDENTITY.Human;
    }

    public void TurnZombie()
    {
        // Change Layer
        mainBody.layer = LayerMask.NameToLayer(CONSTANT.LAYER_NAME_ZOMBIE);

        // Alter components
        Assert.IsNotNull(zombieVersion);
        Instantiate(zombieVersion, transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
