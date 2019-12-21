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
        // Weapon cycle time over && no overlapping attack
        bool weaponReady = weapon.primaryReady();
        bool prevAttackDone = !animator.GetBool("isAttacking");
        string animStateName = weapon.getPrimaryAnimation();
        if (weaponReady && prevAttackDone)
        {
            animator.Play(animStateName, 0);
        }
    }
    public void initSecondaryAttack()
    {
        if (weapon == null)
        {
            Debug.LogWarning("Weapon null!");
            return;
        }
        // Weapon cycle time over && no overlapping attack
        bool weaponReady = weapon.secondaryReady();
        bool prevAttackDone = !animator.GetBool("isAttacking");
        string animStateName = weapon.getSecondaryAnimation();
        if (weaponReady && prevAttackDone)
        {
            animator.Play(animStateName, 0);
        }
    }

    public void AE_primaryAttack()
    {
        LayerMask targetFilter = identity.getTeamComponent().GetOpponentLayerMask();
        weapon.PrimaryAttack(targetFilter, identity);
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

    public void Equip(GameObject weaponPrefab)
    {
        GameObject newWeapon = Instantiate(weaponPrefab, transform);
        newWeapon.transform.localPosition = new Vector3(0,0,0);
        newWeapon.transform.localRotation = Quaternion.identity;
        weapon = newWeapon.GetComponent<Abstract_Weapon>();
    }
}
