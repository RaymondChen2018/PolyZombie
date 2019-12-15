using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Equipment : MonoBehaviour {
    [SerializeField] private Abstract_Weapon weapon;
    [SerializeField] private Abstract_Identity identity;
    [SerializeField] private float damageMultiplier = 1.0f;
    [SerializeField] Animator animator;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(animator);
        Assert.IsNotNull(identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PrimaryAttack(LayerMask targetFilter)
    {
        if(weapon == null)
        {
            Debug.LogWarning("Weapon null!");
            return;
        }

        if(weapon.primaryReady())
        {
            weapon.PrimaryAttack(targetFilter, identity);

            // Animation
            string attackAnimation = weapon.getPrimaryAttackAnimation();
            Assert.IsTrue(attackAnimation != "");
            animator.SetTrigger(attackAnimation);
        }
    }

    public void SecondaryAttack(LayerMask targetFilter)
    {
        if (weapon == null)
        {
            Debug.LogWarning("Weapon null!");
            return;
        }

        if (weapon.secondaryReady())
        {
            weapon.SecondaryAttack(targetFilter, identity);

            // Animation
            string attackAnimation = weapon.getSecondaryAttackAnimation();
            Assert.IsTrue(attackAnimation != "");
            animator.SetTrigger(attackAnimation);
        }
    }

    public bool hasWeapon()
    {
        return weapon != null;
    }

    public float getPrimaryRange() { return weapon.getPrimaryRange(); }
    public void setDamageMultiplier(float value) { damageMultiplier = value; }
}
