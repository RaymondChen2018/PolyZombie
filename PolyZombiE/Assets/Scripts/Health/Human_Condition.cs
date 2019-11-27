using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_Condition : Physical_Condition {
    /// <summary>
    ///  Human with 100.0f infectedness will turn zombie (cap at 100.0f); Once >0.0f, infectedness auto-increase over time
    /// </summary>
    [SerializeField] private float infectedness = 0.0f;

    // Use this for initialization
    void Start () {
        ResetCondition();
    }
	
	// Update is called once per frame
	void Update () {
        // Infection spread
		if(infectedness > 0.0f)
        {
            infectedness *= (1 + CONSTANT.INFECTION_SPREAD_RATIO);
        }

        // If infected
        if (health <= 0.0f)
        {
            status = STATUS.Dead;
        }
        else if (infectedness >= 100.0f)
        {
            status = STATUS.Infected;
        }
	}

    override protected void ResetCondition()
    {
        base.ResetCondition();
        status = STATUS.Healthy;
        infectedness = 0.0f;
    }

    /// <summary>
    /// Spread infectedness on human. Deals a portion of the specified damage.
    /// Deals no damage nor infection to armored unit.
    /// </summary>
    /// <param name="biteDamage"> Damage of bite (Decrease health) </param>
    /// <param name="infectiousness"> Poison of bite (Increase tendancy to turn) </param>
    public void Bit(float biteDamage, float infectiousness)
    {
        if(armor > 0.0f)
        {
            base.Attacked(biteDamage);
            infectedness += infectiousness;
        }
    }
}
