using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[System.Serializable]
public class AttackInfo
{
    public AttackInfo(Abstract_Identity _activator)
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
public class UnityEventAttack : UnityEvent<AttackInfo>
{

}



public class Weapon : MonoBehaviour {
    enum AttackMode
    {
        Primary,
        Secondary
    }

    [Header("Stat")]
    [SerializeField] protected float damagePrimary = 10.0f;
    [SerializeField] protected float damageSecondary = 10.0f;
    [SerializeField] protected float primaryCycleTime = 0.0f;
    [SerializeField] protected float secondaryCycleTime = 0.0f;
    [Tooltip("(Optional, for melee mainly) The max number of affected targets per attack")]
    [Range(1, 10)] [SerializeField] protected int hitMultiple = 1;
    private float prevUseTime = 0.0f;

    [Header("Animation")]
    [SerializeField] protected string primaryAnimation = "";
    [SerializeField] protected string secondaryAnimation = "";

    [Header("Attachment")]
    [Tooltip("Point of interest; Muzzle spot for firearm; Collision detection for melee (Needs a collider)")]
    /// <summary>
    /// Point of interest; for melee, this is damage trace, for projectile weapon, this is muzzle
    /// </summary>
    [SerializeField] protected Transform meleeHitBox;

    [SerializeField] private UnityEventAttack OnPrimary = new UnityEventAttack();
    [SerializeField] private UnityEventAttack OnSecondary = new UnityEventAttack();
    [SerializeField] private UnityEventAttack OnHitPrimary = new UnityEventAttack();
    [SerializeField] private UnityEventAttack OnHitSecondary = new UnityEventAttack();

    AttackMode attackmode = AttackMode.Primary;


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
    virtual public void PrimaryAttack(AttackInfo attackInfo)
    {
        prevUseTime = Time.time;
        attackInfo.damage = damagePrimary * attackInfo.activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;

        attackmode = AttackMode.Primary;
        OnPrimary.Invoke(attackInfo);
    }
    virtual public void SecondaryAttack(AttackInfo attackInfo)
    {
        prevUseTime = Time.time;
        attackInfo.damage = damageSecondary * attackInfo.activator.getEquipmentComponent().getDamageMultiplierPercent() / 100.0f;

        attackmode = AttackMode.Secondary;
        OnSecondary.Invoke(attackInfo);
    }

    // Attack in prograss
    public void Melee(AttackInfo attackInfo)
    {
        Collider2D[] colliders = new Collider2D[hitMultiple];
        Collider2D meleeBox = meleeHitBox.GetComponent<Collider2D>();
        Assert.IsNotNull(meleeBox);
        ContactFilter2D contactFilter = new ContactFilter2D();
        LayerMask enemyFilter = attackInfo.activator.getTeamComponent().GetOpponentLayerMask();
        contactFilter.SetLayerMask(enemyFilter);
        int hitCount = Physics2D.OverlapCollider(meleeBox, contactFilter, colliders);

        for (int i = 0; i < hitCount; i++)
        {
            // Victim
            attackInfo.victim = colliders[i];

            // On hit call back
            switch (attackmode)
            {
                case AttackMode.Primary:
                    OnHitPrimary.Invoke(attackInfo);
                    break;
                case AttackMode.Secondary:
                    OnHitSecondary.Invoke(attackInfo);
                    break;
            }

        }
    }
}
