using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Equipment : MonoBehaviour {
    [SerializeField] private Abstract_Weapon weapon;
    [SerializeField] private Abstract_Identity identity;
    [SerializeField] private int damageMultiplierPercent = 100;
    [SerializeField] Animator animator;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(animator);
        Assert.IsNotNull(identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initPrimaryAttack()
    {
        if(weapon == null)
        {
            Debug.LogWarning("Weapon null!");
            return;
        }
        // Player ready for attac && weapon ready
        if (weapon.primaryReady())
        {
            // Animation
            animator.SetInteger("AttackAnim", 0);
            animator.SetBool("Attack", true);
        }
    }
    public void AE_primaryAttack()
    {
        LayerMask targetFilter = identity.getTeamComponent().GetOpponentLayerMask();
        weapon.PrimaryAttack(targetFilter, identity);
    }

    public void initSecondaryAttack()
    {
        if (weapon == null)
        {
            Debug.LogWarning("Weapon null!");
            return;
        }
        // Player ready for attac && weapon ready
        if (weapon.secondaryReady())
        {
            // Animation
            animator.SetInteger("AttackAnim", 1);
            animator.SetBool("Attack", true);
        }
    }
    public void AE_secondaryAttack()
    {
        LayerMask targetFilter = identity.getTeamComponent().GetOpponentLayerMask();
        weapon.SecondaryAttack(targetFilter, identity);
    }

    public bool hasWeapon()
    {
        return weapon != null;
    }

    public Abstract_Weapon getWeapon() { return weapon; }
    public void setDamageMultiplierPercent(int value) { damageMultiplierPercent = value; }
    public int getDamageMultiplierPercent() { return damageMultiplierPercent; }
}
