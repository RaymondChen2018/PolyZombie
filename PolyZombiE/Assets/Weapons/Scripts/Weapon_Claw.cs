using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Weapon_Claw : Abstract_Weapon {
    [SerializeField] float damage = 5.0f;
    [SerializeField] float biteDamage = 2.0f;

    public override void PrimaryAttack(AttackVictim attackInfo)
    {
        attackInfo.damage = damage;
        AttackPrototype_Melee(attackInfo, Func_OnHitHurt);
    }

    public override void SecondaryAttack(AttackVictim attackInfo)
    {
        attackInfo.damage = biteDamage;
        AttackPrototype_Melee(attackInfo, Func_OnHitInfectHurt);
    }
}
