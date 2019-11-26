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
        status = STATUS.Infected;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    override protected void ResetCondition() 
    {
        base.ResetCondition();
        infectiousness = CONSTANT.MINIMUM_INFECTIOUSNESS;
    }
}
