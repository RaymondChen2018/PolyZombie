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

public class Team_Attribute : MonoBehaviour, ICharacter
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

    //public void SetTeam(Team newTeam)
    //{
    //    switch (newTeam)
    //    {
    //        case Team.Human:
    //            mainBody.layer = LayerMask.NameToLayer(CONSTANT.LAYER_NAME_HUMAN);
    //            break;
    //        case Team.Zombie:
    //            mainBody.layer = LayerMask.NameToLayer(CONSTANT.LAYER_NAME_ZOMBIE);
    //            break;
    //    }
    //    privAnalyzeTeam();
    //}

    //void privAnalyzeTeam()
    //{
    //    string tmpLayerName = LayerMask.LayerToName(mainBody.layer);
    //    switch (tmpLayerName)
    //    {
    //        case CONSTANT.LAYER_NAME_ZOMBIE:
    //            team = Team.Zombie;
    //            break;
    //        case CONSTANT.LAYER_NAME_HUMAN:
    //            team = Team.Human;
    //            break;
    //        default:
    //            Assert.IsTrue(false);
    //            break;
    //    }
    //}

    public Team GetTeam()
    {
        return this.team;
    }

    public LayerMask GetOpponentLayerMask()
    {
        return this.opponentLayerMask;
    }

    public void OnChangeFaction(Identity newFaction)
    {
        Assert.IsTrue(newFaction != Identity.Not_Initialized);
        switch (newFaction)
        {
            case Identity.Human:
                this.team = Team.Human;
                break;
            case Identity.Zombie:
                this.team = Team.Zombie;
                break;
        }
    }

    public void Func_OnDeath()
    {
        //throw new System.NotImplementedException();
    }
}
