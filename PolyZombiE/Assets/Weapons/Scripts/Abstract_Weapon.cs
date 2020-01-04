using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using System;

[System.Serializable]
public class Attack
{
    public Attack() { }
    public Attack(float _damage, Abstract_Identity _victim, Abstract_Identity _activator)
    {
        damage = _damage;
        victim = _victim;
        activator = _activator;
    }
    public Attack(Abstract_Identity _victim, Abstract_Identity _activator)
    {
        victim = _victim;
        activator = _activator;
    }
    public Attack(Abstract_Identity _activator)
    {
        activator = _activator;
    }
    public float damage = 0;
    public Abstract_Identity victim = null;
    public Abstract_Identity activator = null;
}
[System.Serializable]
public class UnityEventAttack : UnityEvent<Attack>
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

    public string getPrimaryAnimation()
    {
        return primaryAnimation;
    }
    public string getSecondaryAnimation()
    {
        return secondaryAnimation;
    }
    protected Vector2 getDirectionVec()
    {
        float rotationAngle = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        return new Vector2(Mathf.Cos(rotationAngle), Mathf.Sin(rotationAngle));
    }
    public void PrimaryAttack(Attack attackInfo)
    {
        prevUseTime = Time.time;
        Func_OnPrimary(attackInfo);
    }
    public void SecondaryAttack(Attack attackInfo)
    {
        prevUseTime = Time.time;
        Func_OnSecondary(attackInfo);
    }
    public bool primaryReady() { return Time.time > prevUseTime + primaryCycleTime; }
    public bool secondaryReady() { return Time.time > prevUseTime + secondaryCycleTime; }
    abstract public void Func_OnPrimary(Attack attackInfo);
    abstract public void Func_OnSecondary(Attack attackInfo);

    // Attack Detection prototypes
    protected void AttackPrototype_Melee(Attack attackInfo, UnityAction<Attack> OnHit)
    {
        Collider2D[] colliders = new Collider2D[hitMultiple];
        Collider2D meleeBox = POI.GetComponent<Collider2D>();
        Assert.IsNotNull(meleeBox);
        ContactFilter2D contactFilter = new ContactFilter2D();
        LayerMask enemyFilter = attackInfo.activator.getTeamComponent().GetOpponentLayerMask();
        contactFilter.SetLayerMask(enemyFilter);
        int hitCount = Physics2D.OverlapCollider(meleeBox, contactFilter, colliders);

        for (int i = 0; i < hitCount; i++)
        {
            Abstract_Identity victim = colliders[i].GetComponent<Abstract_Identity>();
            attackInfo.victim = victim;
            Assert.IsNotNull(OnHit);
            OnHit(attackInfo);
        }
    }
    protected void AttackPrototype_Projectile(Attack attackInfo, UnityAction<Attack> OnHit)
    {
        Assert.IsNotNull(projectilePrefab);
        GameObject projectile = Instantiate(projectilePrefab, POI.position, POI.rotation);
        Projectile_Bullet projectileBullet = projectile.GetComponent<Projectile_Bullet>();
        Assert.IsNotNull(projectileBullet);
        projectileBullet.SetInfo(attackInfo);
        projectileBullet.OnHit.AddListener(OnHit);
    }
}
