using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_Condition : Physical_Condition {
    /// Human with 100.0f infectedness will turn zombie (cap at 100.0f); Once >0.0f, infectedness auto-increase over time
    [SerializeField] private float infectedness = 0.0f;

    // Use this for initialization
    void Start () {
        status = STATUS.Healthy;
    }
	
	// Update is called once per frame
	void Update () {
		if(infectedness > 0.0f)
        {
            infectedness *= (1 + CONSTANT.INFECTION_SPREAD_RATIO);
        }
	}

    override protected void ResetCondition()
    {
        base.ResetCondition();
        infectedness = 0.0f;
    }

    /// <summary>
    /// Spread infectedness on human. Deals a portion of the specified damage.
    /// Deals no damage nor infection to armored unit.
    /// </summary>
    /// <param name="damage"> Damage of bite (preferrably much less than attack damage) </param>
    public void Bite(float biteDamage, float infectiousness)
    {
        if(armor > 0.0f)
        {
            base.Attack(biteDamage);
            infectedness += infectiousness;
        }
    }


}
