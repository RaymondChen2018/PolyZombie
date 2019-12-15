using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatMenu : MonoBehaviour {
    // Points =======================================================
    /// <summary>
    /// For upgrades, temporary skillset (eg. killing spree)
    /// </summary>
    [SerializeField] private int killPoints = 0;
    [SerializeField] private Text killPointsText;

    /// <summary>
    /// For new strains of upgrades, and gaining teammates
    /// </summary>
    [SerializeField] private int infectPoints = 0;
    [SerializeField] private Text infectPointsText;

    // Stat =======================================================
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private Text maxHealthText;

    [SerializeField] private float mobileSpeed = 900.0f;
    [SerializeField] private Text mobileSpeedText;

    [SerializeField] private float infectiousness = CONSTANT.MINIMUM_INFECTIOUSNESS;
    [SerializeField] private Text infectiousnessText;

    [SerializeField] private float damageMultiplier = 1.0f;
    [SerializeField] private Text damageMultiplierText;

    void Awake()
    {
        SaveBaseStat();
    }

    void OnEnable()
    {
        killPointsText.text = killPoints.ToString();
        infectPointsText.text = infectPoints.ToString();

        maxHealthText.text = maxHealth.ToString();
        mobileSpeedText.text = mobileSpeed.ToString();
        infectiousnessText.text = infectiousness.ToString();
        damageMultiplierText.text = damageMultiplier.ToString();
    }

    // Incrementor
    public void addKill(int value)
    {
        killPoints += value;
        killPointsText.text = killPoints.ToString();
    }

    public void addInfect(int value)
    {
        infectPoints += value;
        infectPointsText.text = infectPoints.ToString();
    }

    public void addMaxHealth(float value)
    {
        maxHealth += value;
        maxHealthText.text = maxHealth.ToString();
    }

    public void addMobileSpeed(float value)
    {
        mobileSpeed += value;
        mobileSpeedText.text = mobileSpeed.ToString();
    }

    public void addInfectiousness(float value)
    {
        infectiousness += value;
        infectiousnessText.text = infectiousness.ToString();
    }

    public void addDamageMultiplier(float value)
    {
        damageMultiplier += value;
        damageMultiplierText.text = damageMultiplier.ToString();
    }

    // Updater
    public void SetZombieStat(Zombie_Identity zIdentity)
    {
        zIdentity.getConditionComponent().setMaxHealth(maxHealth);
        zIdentity.getMovementComponent().setMovementSpeed(mobileSpeed);
        zIdentity.setInfectiousness(infectiousness);
        zIdentity.getEquipmentComponent().setDamageMultiplier(damageMultiplier);
    }

    public void SaveBaseStat()
    {
        // Pass to level transition entity
        LevelTransitionStatistics.setKillPoints(killPoints);
        LevelTransitionStatistics.setInfectPoints(infectPoints);
        LevelTransitionStatistics.setMaxHealth(maxHealth);
        LevelTransitionStatistics.setMobileSpeed(mobileSpeed);
        LevelTransitionStatistics.setInfectiousness(infectiousness);
        LevelTransitionStatistics.setDamageMultiplier(damageMultiplier);
    }

    public void LoadBaseStat()
    {
        // Load from to level transition entity
        killPoints = LevelTransitionStatistics.getKillPoints();
        infectPoints = LevelTransitionStatistics.getInfectPoints();
        maxHealth = LevelTransitionStatistics.getMaxHealth();
        mobileSpeed = LevelTransitionStatistics.getMobileSpeed();
        infectiousness = LevelTransitionStatistics.getInfectiousness();
        damageMultiplier = LevelTransitionStatistics.getDamageMultiplier();
    }
}
