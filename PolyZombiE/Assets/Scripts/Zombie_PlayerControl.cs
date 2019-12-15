using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Zombie_PlayerControl : Abstract_PlayerControl
{
    [SerializeField] private KeyCode keyBite;
    [SerializeField] Zombie_Melee meleeComponentZombie;
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        // Melee
        if (Input.GetKeyDown(keyBite))
        {
            Vector2 dof = orientComponent.GetDOF();
            Assert.IsNotNull(meleeComponentZombie);
            equipment.SecondaryAttack(teamComponent.GetOpponentLayerMask());
            //meleeComponentZombie.BiteRay(transform.position, dof, teamComponent.GetOpponentLayerMask());
        }
    }
}
