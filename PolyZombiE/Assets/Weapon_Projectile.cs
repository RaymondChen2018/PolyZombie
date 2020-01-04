using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon_Projectile : Abstract_Weapon
{
    [Header("Gun stat")]
    [SerializeField] float damage = 10.0f;

    // Attack style
    public override void Func_OnPrimary(Attack attackInfo)
    {
        attackInfo.damage = damage;
        AttackPrototype_Projectile(attackInfo, Func_OnHitSubtractHealth);
    }
    public override void Func_OnSecondary(Attack attackInfo)
    {
        
    }

    // Side Effects
    protected void Func_OnHitSubtractHealth(Attack attack)
    {
        float damageDealt = attack.damage;
        Abstract_Identity activator = attack.activator;
        Abstract_Identity victim = attack.victim;
        float damageScaled = damageDealt * activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;
        victim.getHealthComponent().subtractHealth(new DamageInfo(damageScaled, activator.transform.position - victim.transform.position, activator));
    }

}
