using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StatMenu : MonoBehaviour {
    // Points =======================================================
    /// <summary>
    /// For upgrades, temporary skillset (eg. killing spree)
    /// </summary>
    [SerializeField] private int killPoints = 0;
    [SerializeField] private Text killPointsText;
    [SerializeField] private UnityEvent OnRemainKillPoints = new UnityEvent();
    [SerializeField] private UnityEvent OnDepletedKillPoints = new UnityEvent();
    [SerializeField] private UnityEvent OnMenuFullKillPoints = new UnityEvent();
    private int fullMenuKillPoints = 0;

    /// <summary>
    /// For new strains of upgrades, and gaining teammates
    /// </summary>
    [SerializeField] private int infectPoints = 0;
    [SerializeField] private Text infectPointsText;
    [SerializeField] private UnityEvent OnRemainInfectPoints = new UnityEvent();
    [SerializeField] private UnityEvent OnDepletedInfectPoints = new UnityEvent();

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
        setKills(killPoints + value);
    }
    private void setKills(int value)
    {
        killPoints = value;
        killPointsText.text = killPoints.ToString();

        if (killPoints <= 0)
        {
            OnDepletedKillPoints.Invoke();
            if (killPoints >= fullMenuKillPoints)
            {
                OnMenuFullKillPoints.Invoke();
            }
        }
        else
        {
            OnRemainKillPoints.Invoke();
            if (killPoints >= fullMenuKillPoints)
            {
                OnMenuFullKillPoints.Invoke();
            }
        }
    }

    public void addInfect(int value)
    {
        setInfect(infectPoints + value);
    }
    private void setInfect(int value)
    {
        infectPoints = value;
        infectPointsText.text = infectPoints.ToString();

        if (infectPoints <= 0)
        {
            OnDepletedInfectPoints.Invoke();
        }
        else
        {
            OnRemainInfectPoints.Invoke();
        }
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
        fullMenuKillPoints = LevelTransitionStatistics.getKillPoints();
        setKills(LevelTransitionStatistics.getKillPoints());

        setInfect(LevelTransitionStatistics.getInfectPoints());

        maxHealth = LevelTransitionStatistics.getMaxHealth();
        maxHealthText.text = maxHealth.ToString();
        mobileSpeed = LevelTransitionStatistics.getMobileSpeed();
        mobileSpeedText.text = mobileSpeed.ToString();
        infectiousness = LevelTransitionStatistics.getInfectiousness();
        infectiousnessText.text = infectiousness.ToString();
        damageMultiplier = LevelTransitionStatistics.getDamageMultiplier();
        damageMultiplierText.text = damageMultiplier.ToString();
    }
}
