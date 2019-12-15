using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Zombie_Condition : Abstract_Condition {
    ///// <summary>
    ///// Zombie with higher infected(ious)ness can infect human faster.
    ///// </summary>
    //[SerializeField] private float infectiousness = CONSTANT.MINIMUM_INFECTIOUSNESS;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(identity);
        ResetCondition();
    }
	
	// Update is called once per frame
	void Update () {
        // If cured
        if (health < 0.0f)
        {
            status = HEALTH_STATUS.Dead;
        }
        //else if (infectiousness < CONSTANT.MINIMUM_INFECTIOUSNESS)
        //{
        //    status = HEALTH_STATUS.Healthy;
        //}
    }

    override protected void ResetCondition() 
    {
        base.ResetCondition();
        status = HEALTH_STATUS.Infected;
    }
}
