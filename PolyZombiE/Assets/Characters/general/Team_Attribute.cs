using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public enum Team
{
    Not_Initialized,
    Zombie,
    Human
}

public class Team_Attribute : MonoBehaviour
{
    [SerializeField] GameObject mainBody = null;

    [SerializeField] private Team team;
    [SerializeField] private LayerMask opponentLayerMask;
    

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(mainBody);

        //privAnalyzeTeam();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Team GetTeam()
    {
        return this.team;
    }

    public LayerMask GetOpponentLayerMask()
    {
        return this.opponentLayerMask;
    }
}
