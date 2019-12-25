using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Human_Identity : Abstract_Identity
{
    [Header("Human")]
    [SerializeField] Infection infectionComponent;

    public override IDENTITY GetIdentity()
    {
        return IDENTITY.Human;
    }

    public Infection getInfectionComponent()
    {
        Assert.IsNotNull(infectionComponent);
        return infectionComponent;
    }
}
