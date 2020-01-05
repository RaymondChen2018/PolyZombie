using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon_Projectile : Abstract_Weapon
{
    [Header("Gun stat")]
    [SerializeField] float damage = 10.0f;

    [Header("Call-back")]
    [SerializeField] private UnityEvent OnShoot = new UnityEvent();

    // Attack style
    public override void PrimaryAttack(AttackVictim attackInfo)
    {
        attackInfo.damage = damage;
        OnShoot.Invoke();
        AttackPrototype_Projectile(attackInfo, Func_OnHitHurt);
    }
}
