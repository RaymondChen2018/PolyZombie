using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon_Projectile : Abstract_Weapon
{
    [Header("Gun stat")]
    [SerializeField] float damage = 10.0f;

    // Attack style
    public override void PrimaryAttack(AttackVictim attackInfo)
    {
        attackInfo.damage = damage;
        AttackPrototype_Projectile(attackInfo, Func_OnHitHurt);
    }
}
