using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Weapon_Claw : Abstract_Weapon {
    [SerializeField] float damage = 5.0f;
    [SerializeField] float biteDamage = 2.0f;

    public override void Func_OnPrimary(Attack attackInfo)
    {
        attackInfo.damage = damage;
        AttackPrototype_Melee(attackInfo, sideEffect_Primary);
    }

    public override void Func_OnSecondary(Attack attackInfo)
    {
        attackInfo.damage = biteDamage;
        AttackPrototype_Melee(attackInfo, sideEffect_Secondary);
    }

    protected void sideEffect_Primary(Attack attack)
    {
        float damageDealt = attack.damage;
        Abstract_Identity activator = attack.activator;
        Abstract_Identity victim = attack.victim;
        float damageScaled = damageDealt * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
        victim.getHealthComponent().subtractHealth(new DamageInfo(damageScaled, activator.transform.position - victim.transform.position, activator));
    }

    protected void sideEffect_Secondary(Attack attack)
    {
        float damageDealt = attack.damage;
        Abstract_Identity activator = attack.activator;
        Abstract_Identity victim = attack.victim;
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
        float biteDamageScaled = damageDealt * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
        victimHealthComponent.subtractHealth(new DamageInfo(biteDamageScaled, activator.transform.position - victim.transform.position, activator));
    }
}
