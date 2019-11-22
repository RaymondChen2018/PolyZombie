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
                mainBody.layer = LayerMask.NameToLayer(CONSTANT.HUMAN_LAYER_NAME);
                break;
            case Team.Zombie:
                mainBody.layer = LayerMask.NameToLayer(CONSTANT.ZOMBIE_LAYER_NAME);
                break;
        }
        privAnalyzeTeam();
    }

    void privAnalyzeTeam()
    {
        string tmpLayerName = LayerMask.LayerToName(mainBody.layer);
        switch (tmpLayerName)
        {
            case CONSTANT.ZOMBIE_LAYER_NAME:
                team = Team.Zombie;
                break;
            case CONSTANT.HUMAN_LAYER_NAME:
                team = Team.Human;
                break;
            default:
                Assert.IsTrue(false);
                break;
        }
    }
}
