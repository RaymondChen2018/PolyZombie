using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public enum ANIMATION_EVENT
{
    AE_FireExtinguisherAttack,
    AE_FireExtinguisherChargeAttack
}
public class AE_Helper : MonoBehaviour {
    [SerializeField] private Equipment equipment;

	// Use this for initialization
	void Start () {
        Assert.IsNotNull(equipment);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AE_primaryAttack()
    {
        equipment.AE_primaryAttack();
    }
    public void AE_secondaryAttack()
    {
        equipment.AE_secondaryAttack();
    }
}
