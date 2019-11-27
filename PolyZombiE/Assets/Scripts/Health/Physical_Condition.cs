using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Physical_Condition : MonoBehaviour {
    public enum STATUS{
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

    [SerializeField] protected float health = 20.0f;
    [SerializeField] private float maxHealth = 20.0f;
    [SerializeField] protected float armor = 0.0f;
    [SerializeField] protected STATUS status;

    virtual protected void ResetCondition() {
        health = maxHealth;
    }

    /// <summary>
    /// Deals only damage, does not spread infection
    /// </summary>
    /// <param name="damage"> Amount to decrease health</param>
    public void Attacked(float damage)
    {
        health -= damage;
    }

    public STATUS GetConditionStatus()
    {
        return status;
    }
}
