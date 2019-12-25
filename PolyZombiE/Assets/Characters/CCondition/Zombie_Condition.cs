using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Zombie_Condition : Abstract_Condition {

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
    }

    override protected void ResetCondition() 
    {
        base.ResetCondition();
        status = HEALTH_STATUS.Infected;
    }
}
