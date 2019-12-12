using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Equipment : MonoBehaviour {
    [SerializeField] private Abstract_Weapon weapon;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PrimaryAttack(LayerMask targetFilter)
    {
        if(weapon != null)
        {
            weapon.PrimaryAttack(targetFilter);
        }
        else
        {
            Debug.LogWarning("Weapon null!");
        }
    }

    public void SecondaryAttack(LayerMask targetFilter)
    {
        if (weapon != null)
        {
            weapon.SecondaryAttack(targetFilter);
        }
        else
        {
            Debug.LogWarning("Weapon null!");
        }
    }

    public bool hasWeapon()
    {
        return weapon != null;
    }

    public float getPrimaryRange() { return weapon.getPrimaryRange(); }
}
