using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

public enum HEALTH
{
    /// <summary>
    /// Default status.
    /// </summary>
    Not_Initialized,
    /// <summary>
    /// Healthy
    /// </summary>
    Alive,
    /// <summary>
    /// Deceased.
    /// </summary>
    Dead
}
[System.Serializable]
public class DamageInfo
{
    public float damageAmount;
    /// <summary>
    /// From activator to victim
    /// </summary>
    public Vector2 direction;
    public Abstract_Identity activator;
    public DamageInfo(float _damageAmount, Vector2 _direction, Abstract_Identity _activator)
    {
        damageAmount = _damageAmount;
        direction = _direction;
        activator = _activator;
    }
}
[System.Serializable]
public class UnityEventDamageInfo : UnityEvent<DamageInfo>
{

}
public class Health : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] protected float health = 20.0f;
    [SerializeField] private float maxHealth = 20.0f;
    [SerializeField] protected float armor = 0.0f;
    private bool isDead = false;

    [Header("Output")]
    [SerializeField] protected UnityEvent OnDeath = new UnityEvent();
    [SerializeField] private UnityEventDamageInfo OnDamaged = new UnityEventDamageInfo();

    // Use this for initialization
    void Start () {
        Assert.IsTrue(health > 0.0f);
        Assert.IsTrue(maxHealth > 0.0f);
        Assert.IsTrue(armor >= 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Deals only damage, does not spread infection
    /// </summary>
    /// <param name="damage"> Amount to decrease health</param>
    public void subtractHealth(DamageInfo damageInfo)//float damage, Abstract_Identity activator)
    {
        health -= damageInfo.damageAmount;
        OnDamaged.Invoke(damageInfo);
        if (health <= 0.0f && !isDead)
        {
            // Killer call-back
            damageInfo.activator.Func_OnKilledSomeOne();

            // this call back
            OnDeath.Invoke();
            OnDeath.RemoveAllListeners();

            // 
            isDead = true;
            Destroy(gameObject);
        }
    }

    public void setHealth(float value)
    {
        health = Mathf.Clamp(value, 0.0f, value);
    }
    public float getHealth()
    {
        return health;
    }

    public void setMaxHealth(float value)
    {
        maxHealth = value;
    }
    public float getMaxHealth()
    {
        return maxHealth;
    }

    public float getArmor()
    {
        return armor;
    }
}
