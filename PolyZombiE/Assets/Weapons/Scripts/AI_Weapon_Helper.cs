using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Weapon_Helper : MonoBehaviour {
    [SerializeField] private float attackRange = 5.0f;
    [SerializeField] private bool isRanged = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float getAttackRange()
    {
        return attackRange;
    }

    public bool isRangeWeapon()
    {
        return isRanged;
    }
}
