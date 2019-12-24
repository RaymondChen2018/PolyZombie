using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Zombie_PlayerControl : Abstract_PlayerControl
{
    [SerializeField] private KeyCode keyBite;
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        // Melee
        if (Input.GetKeyDown(keyBite))
        {
            Vector2 dof = orientComponent.GetDOF();
            equipment.initSecondaryAttack();
        }
    }
}
