using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionStatistics : MonoBehaviour {
    private static LevelTransitionStatistics singleton;

    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float mobileSpeed = 900.0f;
    [SerializeField] private float infectiousness = CONSTANT.MINIMUM_INFECTIOUSNESS;
    [SerializeField] private float damageMultiplier = 1.0f;
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
	
	public static void setMaxHealth(float value)
    {
        singleton.maxHealth = value;
    }
    public static float getMaxHealth()
    {
        return singleton.maxHealth;
    }

    public static void setMobileSpeed(float value)
    {
        singleton.mobileSpeed = value;
    }
    public static float getMobileSpeed()
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

    public static void setInfectiousness(float value)
    {
        singleton.infectiousness = value;
    }
    public static float getInfectiousness()
    {
        return singleton.infectiousness;
    }

    public static void setDamageMultiplier(float value)
    {
        singleton.damageMultiplier = value;
    }
    public static float getDamageMultiplier()
    {
        return singleton.damageMultiplier;
    }
}
