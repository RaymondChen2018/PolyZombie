using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEarnings : MonoBehaviour {
    /// <summary>
    /// For upgrades, temporary skillset (eg. killing spree)
    /// </summary>
    [SerializeField] private int peopleKilled = 0;

    /// <summary>
    /// For new strains of upgrades, and gaining teammates
    /// </summary>
    [SerializeField] private int peopleInfected = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addKill()
    {
        peopleKilled++;
    }

    public void addInfect()
    {
        peopleInfected++;
    }
}
