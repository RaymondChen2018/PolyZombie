using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StatMenu : MonoBehaviour {
    // Points =======================================================
    [SerializeField] private Math_Counter killPoints;

    [SerializeField] private Math_Counter infectPoints;

    // Stat =======================================================
    [SerializeField] private Math_Counter maxHealth;

    [SerializeField] private Math_Counter mobileSpeed;

    [SerializeField] private Math_Counter infectiousness;

    [SerializeField] private Math_Counter damageMultiplierPercent;

    void Awake()
    {

    }

    // Updater
    public void SetZombieStat(Zombie_Identity zIdentity)
    {
        zIdentity.getConditionComponent().setMaxHealth(maxHealth.getValue());
        zIdentity.getMovementComponent().setMovementSpeed(mobileSpeed.getValue());
        zIdentity.setInfectiousness(infectiousness.getValue());
        zIdentity.getEquipmentComponent().setDamageMultiplierPercent(damageMultiplierPercent.getValue());
    }

    public void SaveBaseStat()
    {
        // Pass to level transition entity
        LevelTransitionStatistics.setKillPoints(killPoints.getValue());
        LevelTransitionStatistics.setInfectPoints(infectPoints.getValue());
        LevelTransitionStatistics.setMaxHealth(maxHealth.getValue());
        LevelTransitionStatistics.setMobileSpeed(mobileSpeed.getValue());
        LevelTransitionStatistics.setInfectiousness(infectiousness.getValue());
        LevelTransitionStatistics.setDamageMultiplierPercent(damageMultiplierPercent.getValue());
    }

    public void LoadBaseStat()
    {
        // Load from to level transition entity
        killPoints.setValue(LevelTransitionStatistics.getKillPoints());
        infectPoints.setValue(LevelTransitionStatistics.getInfectPoints());

        maxHealth.setMinValue(LevelTransitionStatistics.getMaxHealth());
        maxHealth.setValue(LevelTransitionStatistics.getMaxHealth());

        mobileSpeed.setMinValue(LevelTransitionStatistics.getMobileSpeed());
        mobileSpeed.setValue(LevelTransitionStatistics.getMobileSpeed());

        infectiousness.setMinValue(LevelTransitionStatistics.getInfectiousness());
        infectiousness.setValue(LevelTransitionStatistics.getInfectiousness());

        damageMultiplierPercent.setMinValue(LevelTransitionStatistics.getDamageMultiplier());
        damageMultiplierPercent.setValue(LevelTransitionStatistics.getDamageMultiplier());
    }
}
