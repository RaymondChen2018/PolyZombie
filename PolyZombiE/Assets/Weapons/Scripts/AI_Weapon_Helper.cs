using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Weapon_Helper : MonoBehaviour {
    [SerializeField] private float attackRange = 5.0f;

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
}
