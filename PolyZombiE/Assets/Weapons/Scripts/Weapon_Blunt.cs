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
    public override void PrimaryAttack(AttackVictim attackInfo)
    {
        attackInfo.damage = damage;
        AttackPrototype_Melee(attackInfo, Func_OnHitHurt);
    }
    public override void SecondaryAttack(AttackVictim attackInfo)
    {
        attackInfo.damage = damageBig;
        AttackPrototype_Melee(attackInfo, Func_OnHitHurt);
    }
}
