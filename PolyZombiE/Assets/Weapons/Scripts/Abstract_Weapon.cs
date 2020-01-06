using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using System;

[System.Serializable]
public class AttackVictim
{
    public AttackVictim(Abstract_Identity _activator)
    {
        activator = _activator;
        posImpact = new Vector2();
    }
    public float damage = 0;
    public Vector2 posImpact;
    public Collider2D victim = null;
    public Abstract_Identity activator = null;
}


[System.Serializable]
public class UnityEventAttack : UnityEvent<AttackVictim>
{

}

public abstract class Abstract_Weapon: MonoBehaviour{
    [Header("Stat")]
    [SerializeField] protected float primaryCycleTime = 0.0f;
    [SerializeField] protected float secondaryCycleTime = 0.0f;
    [Tooltip("(Optional, for melee mainly) The max number of affected targets per attack")]
    [Range(1,10)][SerializeField] protected int hitMultiple = 1;
    private float prevUseTime = 0.0f;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;

    [Header("Animation")]
    [SerializeField] protected string primaryAnimation = "";
    [SerializeField] protected string secondaryAnimation = "";

    [Header("Attachment")]
    [Tooltip("Point of interest; Muzzle spot for firearm; Collision detection for melee (Needs a collider)")]
    /// <summary>
    /// Point of interest; for melee, this is damage trace, for projectile weapon, this is muzzle
    /// </summary>
    [SerializeField] protected Transform POI;

    // Getters
    public string getPrimaryAnimation()
    {
        return primaryAnimation;
    }
    public string getSecondaryAnimation()
    {
        return secondaryAnimation;
    }
    public bool primaryReady() { return Time.time > prevUseTime + primaryCycleTime; }
    public bool secondaryReady() { return Time.time > prevUseTime + secondaryCycleTime; }

    // Attack start
    virtual public void PrimaryAttack(AttackVictim attackInfo) { }
    virtual public void SecondaryAttack(AttackVictim attackInfo) { }

    // Attack in prograss
    protected void AttackPrototype_Melee(AttackVictim attackInfo, UnityAction<AttackVictim> OnHit)
    {
        prevUseTime = Time.time;
        Collider2D[] colliders = new Collider2D[hitMultiple];
        Collider2D meleeBox = POI.GetComponent<Collider2D>();
        Assert.IsNotNull(meleeBox);
        ContactFilter2D contactFilter = new ContactFilter2D();
        LayerMask enemyFilter = attackInfo.activator.getTeamComponent().GetOpponentLayerMask();
        contactFilter.SetLayerMask(enemyFilter);
        int hitCount = Physics2D.OverlapCollider(meleeBox, contactFilter, colliders);

        for (int i = 0; i < hitCount; i++)
        {
            attackInfo.victim = colliders[i];
            Assert.IsNotNull(OnHit);
            OnHit(attackInfo);
        }
    }
    protected void AttackPrototype_Projectile(AttackVictim attackInfo, UnityAction<AttackVictim> OnHit)
    {
        prevUseTime = Time.time;
        Assert.IsNotNull(projectilePrefab);
        GameObject projectile = Instantiate(projectilePrefab, POI.position, POI.rotation);
        Projectile_Bullet projectileBullet = projectile.GetComponent<Projectile_Bullet>();
        Assert.IsNotNull(projectileBullet);
        projectileBullet.SetInfo(attackInfo);
        projectileBullet.OnHit.AddListener(OnHit);
    }

    // Attack End
    protected void Func_OnHitHurt(AttackVictim attack)
    {
        if(attack.activator == null)
        {
            attack.activator = World_Identity.singleton;
        }

        Assert.IsNotNull(attack.activator);
        Assert.IsNotNull(attack.victim);

        Abstract_Identity activator = attack.activator;
        Abstract_Identity victim = attack.victim.GetComponent<Abstract_Identity>();

        // Hit character
        if(victim != null)
        {
            float damageDealt = attack.damage;
            float damageMultiplier = activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;

            victim.getHealthComponent().subtractHealth(new DamageInfo(damageDealt * damageMultiplier, activator.transform.position - victim.transform.position, activator));
        }
        else
        {

        }
    }

    protected void Func_OnHitInfectHurt(AttackVictim attack)
    {
        float damageDealt = attack.damage;
        Abstract_Identity activator = attack.activator;
        // Victim must be human
        Human_Identity victim = attack.victim.GetComponent<Human_Identity>();
        if(victim != null)
        {
            Health victimHealthComponent = victim.getHealthComponent();
            Infection victimInfectionComponent = victim.getInfectionComponent();
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
        else
        {

        }
    }
}
