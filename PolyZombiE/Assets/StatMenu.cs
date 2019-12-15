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
    private int prevKillPoints;

    /// <summary>
    /// For new strains of upgrades, and gaining teammates
    /// </summary>
    [SerializeField] private int infectPoints = 0;
    [SerializeField] private Text infectPointsText;
    private int prevInfectPoints;

    // Stat =======================================================
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private Text maxHealthText;
    private float prevMaxHealth;

    [SerializeField] private float mobileSpeed = 900.0f;
    [SerializeField] private Text mobileSpeedText;
    private float prevMobileSpeed;

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
    }

    public void SaveBaseStat()
    {
        prevKillPoints = killPoints;
        prevInfectPoints = infectPoints;
        prevMaxHealth = maxHealth;
        prevMobileSpeed = mobileSpeed;
    }

    public void LoadBaseStat()
    {
        killPoints = prevKillPoints;
        infectPoints = prevInfectPoints;
        maxHealth = prevMaxHealth;
        mobileSpeed = prevMobileSpeed;
    }


    // Incrementor
    public void addKill()
    {
        killPoints++;
        killPointsText.text = killPoints.ToString();
    }

    public void addInfect()
    {
        infectPoints++;
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

    // Updater
    public void GetZombieStat(Zombie_Identity zIdentity)
    {
        
    }

    public void SetZombieStat(Zombie_Identity zIdentity)
    {

    }
}
