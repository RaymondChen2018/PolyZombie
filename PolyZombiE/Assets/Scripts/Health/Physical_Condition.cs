using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Physical_Condition : MonoBehaviour {
    public enum STATUS{
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
        /// Deceased
        /// </summary>
        Dead
    }

    [SerializeField] private float health = 20.0f;
    [SerializeField] private float maxHealth = 20.0f;
    [SerializeField] protected float armor = 0.0f;
    [SerializeField] protected STATUS status;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual protected void ResetCondition() {
        health = maxHealth;
    }

    /// <summary>
    /// Deals only damage, does not spread infection
    /// </summary>
    /// <param name="damage"></param>
    public void Attack(float damage)
    {
        health -= damage;
    }
}
