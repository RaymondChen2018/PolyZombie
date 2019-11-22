using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class Team_Switcher : MonoBehaviour {


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void SetTeam(Team_Attribute teamComponent, Team newTeam)
    {
        teamComponent.SetTeam(newTeam);
    }
}
