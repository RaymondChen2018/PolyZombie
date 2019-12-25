using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public abstract class Abstract_Weapon: MonoBehaviour{
    [Header("Stat")]
    [SerializeField] protected float primaryCycleTime = 0.0f;
    [SerializeField] protected float secondaryCycleTime = 0.0f;
    [Tooltip("(Optional, for melee mainly) The max number of affected targets per attack")]
    [Range(1,10)][SerializeField] protected int hitMultiple = 1;
    private float prevUseTime = 0.0f;

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
    public void PrimaryAttack(LayerMask targetFilter, Abstract_Identity activator)
    {
        prevUseTime = Time.time;
        Func_OnPrimary(targetFilter, activator);
    }
    public void SecondaryAttack(LayerMask targetFilter, Abstract_Identity activator)
    {
        prevUseTime = Time.time;
        Func_OnSecondary(targetFilter, activator);
    }
    public bool primaryReady() { return Time.time > prevUseTime + primaryCycleTime; }
    public bool secondaryReady() { return Time.time > prevUseTime + secondaryCycleTime; }
    abstract public void Func_OnPrimary(LayerMask targetFilter, Abstract_Identity activator);
    abstract public void Func_OnSecondary(LayerMask targetFilter, Abstract_Identity activator);

    // Attack Detection prototypes
    protected void AttackPrototype_Melee(LayerMask targetFilter, Abstract_Identity activator, Action<Abstract_Condition, Abstract_Identity> OnHit)
    {
        Collider2D[] colliders = new Collider2D[hitMultiple];
        Collider2D meleeBox = POI.GetComponent<Collider2D>();
        Assert.IsNotNull(meleeBox);
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(targetFilter);
        int hitCount = Physics2D.OverlapCollider(meleeBox, contactFilter, colliders);

        for (int i = 0; i < hitCount; i++)
        {
            Abstract_Condition cCondition = colliders[i].GetComponent<Abstract_Condition>();
            Assert.IsNotNull(OnHit);
            OnHit(cCondition, activator);
        }
    }
    protected void AttackPrototype_Projectile(){}
}
