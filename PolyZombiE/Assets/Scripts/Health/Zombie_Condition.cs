using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Condition : Physical_Condition {
    /// <summary>
    /// Zombie with higher infected(ious)ness can infect human faster.
    /// </summary>
    [SerializeField] private float infectiousness = CONSTANT.MINIMUM_INFECTIOUSNESS;

    // Use this for initialization
    void Start () {
        ResetCondition();
    }
	
	// Update is called once per frame
	void Update () {
        // If cured
        if (health < 0.0f)
        {
            status = STATUS.Dead;
        }
        else if (infectiousness < CONSTANT.MINIMUM_INFECTIOUSNESS)
        {
            status = STATUS.Healthy;
        }
    }

    override protected void ResetCondition() 
    {
        base.ResetCondition();
        status = STATUS.Infected;
        infectiousness = CONSTANT.MINIMUM_INFECTIOUSNESS;
    }
}
