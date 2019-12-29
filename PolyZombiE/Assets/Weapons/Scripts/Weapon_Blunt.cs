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
    public override void Func_OnPrimary(LayerMask targetFilter, Abstract_Identity activator)
    {
        AttackPrototype_Melee(targetFilter, activator, sideEffect_Primary);
    }
    public override void Func_OnSecondary(LayerMask targetFilter, Abstract_Identity activator)
    {
        AttackPrototype_Melee(targetFilter, activator, sideEffect_Secondary);
    }

    // Side Effects
    protected void sideEffect_Primary(Abstract_Identity victim, Abstract_Identity activator)
    {
        float damageScaled = damage * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
        victim.getHealthComponent().subtractHealth(new DamageInfo(damageScaled, activator.transform.position - victim.transform.position, activator));
    }
    protected void sideEffect_Secondary(Abstract_Identity victim, Abstract_Identity activator)
    {
        float damageScaled = damageBig * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
        victim.getHealthComponent().subtractHealth(new DamageInfo(damageScaled, activator.transform.position - victim.transform.position, activator));
    }
}
