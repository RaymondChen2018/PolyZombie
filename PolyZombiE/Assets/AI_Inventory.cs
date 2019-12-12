using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AI_Inventory : MonoBehaviour {
    [SerializeField] Equipment equipment;
    [SerializeField] Animator AIStateMachine;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(equipment);
        Assert.IsNotNull(AIStateMachine);
	}
	
	// Update is called once per frame
	void Update () {
        AIStateMachine.SetBool("Weaponized", equipment.hasWeapon());
	}
}
