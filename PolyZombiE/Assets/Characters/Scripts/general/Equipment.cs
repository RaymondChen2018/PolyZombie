using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventWeapon : UnityEvent<Abstract_Weapon>
{

}
public class Equipment : MonoBehaviour {
    [SerializeField] private Abstract_Weapon weapon;
    [SerializeField] private Abstract_Identity identity;
    [SerializeField] private Transform WeaponBoneR;
    [SerializeField] private LayerMask weaponLayerMask;
    [SerializeField] Animator animator;

    [Header("Stat")]
    [SerializeField] private int damageMultiplierPercent = 100;
    [SerializeField] private float pickUpRadius = 2.0f;

    [SerializeField] UnityEventWeapon OnEquip = new UnityEventWeapon();

    [Header("Debug")]
    [SerializeField] private bool debugOn = false;
    [SerializeField] private Color debugColor = Color.red;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(animator);
        Assert.IsNotNull(identity);
        Assert.IsNotNull(WeaponBoneR);
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
        weapon.PrimaryAttack(new AttackVictim(identity));
    }
    public void AE_secondaryAttack()
    {
        weapon.SecondaryAttack(new AttackVictim(identity));
    }

    public bool hasWeapon()
    {
        return weapon != null;
    }

    public Abstract_Weapon getWeapon() { return weapon; }
    public void setDamageMultiplierPercent(int value) { damageMultiplierPercent = value; }
    public int getDamageMultiplierPercent() { return damageMultiplierPercent; }
    public float getPickUpRadius()
    {
        return pickUpRadius;
    }

    public void SpawnEquip(GameObject weaponPrefab)
    {
        // Create weapon
        GameObject newWeapon = Instantiate(weaponPrefab);
        weapon = newWeapon.GetComponent<Abstract_Weapon>();

        // Attach to parent bone
        Attachment_Helper attachmentHelper = newWeapon.GetComponent<Attachment_Helper>();
        attachmentHelper.SetAttachment(WeaponBoneR);
    }

    public void Equip(Abstract_Weapon newWeapon)
    {

        OnEquip.Invoke(newWeapon);
        // Call weapon equip
        Weapon_Equip_Helper equipHelper = newWeapon.GetComponent<Weapon_Equip_Helper>();
        if(equipHelper.getUser() == null)
        {
            equipHelper.setUser(identity);

            // Assign
            weapon = newWeapon;

            // Attach to parent bone
            Attachment_Helper attachmentHelper = newWeapon.GetComponent<Attachment_Helper>();
            attachmentHelper.SetAttachment(WeaponBoneR);
        }
    }
    public void Dequip()
    {
        if(weapon!= null)
        {
            // Attach to parent bone
            Attachment_Helper attachmentHelper = weapon.GetComponent<Attachment_Helper>();
            attachmentHelper.SetAttachment(null);

            // Call weapon equip
            Weapon_Equip_Helper equipHelper = weapon.GetComponent<Weapon_Equip_Helper>();
            equipHelper.setUser(null);

            // Nullify link
            weapon = null;
        }
    }

    public void pickUp()
    {
        Collider2D weaponCollider = Physics2D.OverlapCircle(identity.getMovementComponent().getPosition(), pickUpRadius, weaponLayerMask);
        if (weaponCollider)
        {
            Abstract_Weapon weaponAbstract = weaponCollider.GetComponent<Abstract_Weapon>();
            Assert.IsNotNull(weaponAbstract);
            Equip(weaponAbstract);
        }
    }

    private void OnDrawGizmos()
    {
        if (debugOn)
        {
            AI_Finder.DrawEllipse(transform.position, pickUpRadius, debugColor);
        }
    }
}
