using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum Team
{
    Zombie,
    Human
}

public class Team_Attribute : MonoBehaviour {
    [SerializeField] GameObject mainBody = null;

    [SerializeField] private LayerMask opponentLayerMask;
    private Team team;
    
    
	// Use this for initialization
	void Start () {
        Assert.IsNotNull(mainBody);

        // Layer name invalid
        privAnalyzeTeam();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTeam(Team newTeam)
    {
        switch (newTeam)
        {
            case Team.Human:
                mainBody.layer = LayerMask.NameToLayer(CONSTANT.LAYER_NAME_HUMAN);
                break;
            case Team.Zombie:
                mainBody.layer = LayerMask.NameToLayer(CONSTANT.LAYER_NAME_ZOMBIE);
                break;
        }
        privAnalyzeTeam();
    }

    void privAnalyzeTeam()
    {
        string tmpLayerName = LayerMask.LayerToName(mainBody.layer);
        switch (tmpLayerName)
        {
            case CONSTANT.LAYER_NAME_ZOMBIE:
                team = Team.Zombie;
                break;
            case CONSTANT.LAYER_NAME_HUMAN:
                team = Team.Human;
                break;
            default:
                Assert.IsTrue(false);
                break;
        }
    }

    public LayerMask GetOpponentLayerMask()
    {
        return opponentLayerMask;
    }
}
