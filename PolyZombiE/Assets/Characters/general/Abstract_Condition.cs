using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum HEALTH_STATUS
{
    /// <summary>
    /// Default status.
    /// </summary>
    Not_Initialized,
    /// <summary>
    /// Human healthy status; zombie with this status turns human.
    /// </summary>
    Healthy,
    /// <summary>
    /// Zombie healthy status, human with this status turns zombie.
    /// </summary>
    Infected,
    /// <summary>
    /// Deceased.
    /// </summary>
    Dead
}

public abstract class Abstract_Condition : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] protected float health = 20.0f;
    [SerializeField] private float maxHealth = 20.0f;
    [SerializeField] protected float armor = 0.0f;
    [SerializeField] protected HEALTH_STATUS status;

    [Header("Pointer")]
    [SerializeField] protected Abstract_Identity identity;

    [Header("Output")]
    [SerializeField] protected UnityEvent OnDeathOnce = new UnityEvent();

    virtual protected void ResetCondition() {
        health = maxHealth;
    }

    /// <summary>
    /// Deals only damage, does not spread infection
    /// </summary>
    /// <param name="damage"> Amount to decrease health</param>
    public void subtractHealth(float damage, Abstract_Identity activator)
    {
        health -= damage;
        if (health <= 0.0f && status != HEALTH_STATUS.Dead)
        {
            activator.Func_OnKilledSomeOne();
            Func_OnDeath();
            // identity
            identity.Die();
        }
    }

    public HEALTH_STATUS GetConditionStatus()
    {
        return status;
    }

    public void Func_OnDeath()
    {
        status = HEALTH_STATUS.Dead;

        // Custom call back
        OnDeathOnce.Invoke();
        OnDeathOnce.RemoveAllListeners();
    }
    public void setMaxHealth(float value)
    {
        maxHealth = value;
    }
}
