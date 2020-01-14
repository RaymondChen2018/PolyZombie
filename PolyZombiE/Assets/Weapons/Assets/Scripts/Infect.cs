using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Infect : MonoBehaviour, IWeapon_effector {
    public void inflict(AttackInfo attackInfo)
    {
        Abstract_Identity activator = attackInfo.activator;
        // Victim must be human
        Human_Identity victim = attackInfo.victim.GetComponent<Human_Identity>();
        if (victim != null)
        {
            Infection victimInfectionComponent = victim.getInfectionComponent();
            Assert.IsNotNull(victimInfectionComponent);
            Zombie_Identity activatorZomb = (Zombie_Identity)activator;
            Assert.IsNotNull(activatorZomb);

            // Infect
            float infectiousness = activatorZomb.GetInfectiousness();
            victimInfectionComponent.addInfection(infectiousness, activatorZomb);
        }
    }
}
