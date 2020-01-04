using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Weapon_Blunt : Abstract_Weapon
{
    [Header("Melee Stat")]
    [SerializeField] float damage = 5.0f;
    [SerializeField] float damageBig = 10.0f;

    // Attack style
    public override void Func_OnPrimary(Attack attackInfo)
    {
        attackInfo.damage = damage;
        AttackPrototype_Melee(attackInfo, sideEffect_Primary);
    }
    public override void Func_OnSecondary(Attack attackInfo)
    {
        attackInfo.damage = damageBig;
        AttackPrototype_Melee(attackInfo, sideEffect_Secondary);
    }

    // Side Effects
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
        float damageScaled = damageDealt * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
        victim.getHealthComponent().subtractHealth(new DamageInfo(damageScaled, activator.transform.position - victim.transform.position, activator));
    }
}
