using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class Infection : MonoBehaviour {
    [Header("Stat")]
    /// <summary>
    ///  Human with 100.0f infectedness will turn zombie (cap at 100.0f); Once >0.0f, infectedness auto-increase over time
    /// </summary>
    [SerializeField] private float infectedness = 0.0f;

    [Header("Output")]
    [SerializeField] private UnityEvent OnInfectOnce = new UnityEvent();

    [SerializeField] private Health health;

    [Header("Infected Version")]
    [SerializeField] private GameObject zombieVersion;

    private bool isInfected = false;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(health);
        Assert.IsNotNull(zombieVersion);
        Assert.IsTrue(infectedness >= 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        // Infection spread
        if (infectedness > 0.0f)
        {
            infectedness *= (1 + CONSTANT.INFECTION_SPREAD_RATIO);
        }

        if (infectedness >= 100.0f && !isInfected)
        {
            isInfected = true;

            OnInfectOnce.Invoke();
            OnInfectOnce.RemoveAllListeners();

            TurnZombie();
        }
    }

    /// <summary>
    /// Spread infectedness on human. Deals a portion of the specified damage.
    /// Deals no damage nor infection to armored unit.
    /// </summary>
    /// <param name="infectiousness"> Poison of bite (Increase tendancy to turn) </param>
    public void addInfection(float infectiousness, Zombie_Identity activator)
    {
        // Infect
        if (health.getArmor() <= 0.0f)
        {
            infectedness += infectiousness;
        }

        UnityAction infectedRelayCall = new UnityAction(activator.Func_OnInfectedSomeOne);
        OnInfectOnce.RemoveListener(infectedRelayCall);
        OnInfectOnce.AddListener(infectedRelayCall);
    }

    public void TurnZombie()
    {
        // Alter components
        Assert.IsNotNull(zombieVersion);
        Instantiate(zombieVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
