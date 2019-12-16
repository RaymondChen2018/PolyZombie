using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionStatistics : MonoBehaviour {
    private static LevelTransitionStatistics singleton;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int mobileSpeed = 900;
    [SerializeField] private int infectiousness = (int)CONSTANT.MINIMUM_INFECTIOUSNESS;
    [SerializeField] private int damageMultiplierPercent = 100;
    [SerializeField] private int killPoints = 0;
    [SerializeField] private int infectPoints = 0;

    // Use this for initialization
    void Awake () {
        if(singleton != null)
        {
            Debug.LogWarning("Multiple leveltransitionentity detected. Resolved");
            Destroy(this);
        }
        else
        {
            singleton = this;

            DontDestroyOnLoad(this);
        }
	}
	
	public static void setMaxHealth(int value)
    {
        singleton.maxHealth = value;
    }
    public static int getMaxHealth()
    {
        return singleton.maxHealth;
    }

    public static void setMobileSpeed(int value)
    {
        singleton.mobileSpeed = value;
    }
    public static int getMobileSpeed()
    {
        return singleton.mobileSpeed;
    }

    public static void setKillPoints(int value)
    {
        singleton.killPoints = value;
    }
    public static int getKillPoints()
    {
        return singleton.killPoints;
    }

    public static void setInfectPoints(int value)
    {
        singleton.infectPoints = value;
    }
    public static int getInfectPoints()
    {
        return singleton.infectPoints;
    }

    public static void setInfectiousness(int value)
    {
        singleton.infectiousness = value;
    }
    public static int getInfectiousness()
    {
        return singleton.infectiousness;
    }

    public static void setDamageMultiplierPercent(int value)
    {
        singleton.damageMultiplierPercent = value;
    }
    public static int getDamageMultiplier()
    {
        return singleton.damageMultiplierPercent;
    }
}
