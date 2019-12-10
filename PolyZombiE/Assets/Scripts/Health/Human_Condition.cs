using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Human_Condition : Abstract_Condition {
    /// <summary>
    ///  Human with 100.0f infectedness will turn zombie (cap at 100.0f); Once >0.0f, infectedness auto-increase over time
    /// </summary>
    [SerializeField] private float infectedness = 0.0f;

    [SerializeField] protected UnityEvent OnInfectOnce = new UnityEvent();

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(identity);
        ResetCondition();
    }
	
	// Update is called once per frame
	void Update () {
        // Infection spread
		if(infectedness > 0.0f)
        {
            infectedness *= (1 + CONSTANT.INFECTION_SPREAD_RATIO);
        }

        if(infectedness >= 100.0f && status != HEALTH_STATUS.Infected)
        {
            OnInfect();
        }
	}

    override protected void ResetCondition()
    {
        base.ResetCondition();
        status = HEALTH_STATUS.Healthy;
        infectedness = 0.0f;
    }

    /// <summary>
    /// Spread infectedness on human. Deals a portion of the specified damage.
    /// Deals no damage nor infection to armored unit.
    /// </summary>
    /// <param name="infectiousness"> Poison of bite (Increase tendancy to turn) </param>
    public void addInfection(float infectiousness)
    {
        // Infect
        if(armor == 0.0f)
        {
            infectedness += infectiousness;
        }
    }

    public override void OnChangeFaction(Identity newFaction)
    {
        Assert.IsTrue(newFaction == Identity.Zombie);
        status = HEALTH_STATUS.Infected;
    }

    public override void OnDeath()
    {
        status = HEALTH_STATUS.Dead;

        // Custom call back
        OnDeathOnce.Invoke();
        OnDeathOnce.RemoveAllListeners();
    }

    public void OnInfect()
    {
        OnInfectOnce.Invoke();
        OnInfectOnce.RemoveAllListeners();

        ((Human_Identity)identity).TurnZombie();
    }
}
