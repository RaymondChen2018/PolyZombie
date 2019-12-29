using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Weapon_Claw : Abstract_Weapon {
    [SerializeField] float damage = 5.0f;
    [SerializeField] float biteDamage = 2.0f;

    public override void Func_OnPrimary(LayerMask targetFilter, Abstract_Identity activator)
    {
        AttackPrototype_Melee(targetFilter, activator, sideEffect_Primary);
    }

    public override void Func_OnSecondary(LayerMask targetFilter, Abstract_Identity activator)
    {
        AttackPrototype_Melee(targetFilter, activator, sideEffect_Secondary);
    }

    protected void sideEffect_Primary(Abstract_Identity victim, Abstract_Identity activator)
    {
        float damageScaled = damage * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
        victim.getHealthComponent().subtractHealth(new DamageInfo(damageScaled, activator.transform.position - victim.transform.position, activator));
    }

    protected void sideEffect_Secondary(Abstract_Identity victim, Abstract_Identity activator)
    {
        // Victim must be human
        Human_Identity victimHuman = (Human_Identity)victim;
        Health victimHealthComponent = victimHuman.getHealthComponent();
        Infection victimInfectionComponent = victimHuman.getInfectionComponent();
        Assert.IsNotNull(victimHealthComponent);
        Zombie_Identity activatorZomb = (Zombie_Identity)activator;
        Assert.IsNotNull(victimHealthComponent);

        // Infect
        float infectiousness = activatorZomb.GetInfectiousness();
        victimInfectionComponent.addInfection(infectiousness, activatorZomb);

        // Damage
        float biteDamageScaled = biteDamage * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
        victimHealthComponent.subtractHealth(new DamageInfo(biteDamageScaled, activator.transform.position - victim.transform.position, activator));
    }
}
