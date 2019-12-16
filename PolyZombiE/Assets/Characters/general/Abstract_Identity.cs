using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public enum Identity
{
    Not_Initialized,
    Human,
    Zombie
}

/// <summary>
/// Manager class of other faction attributes
/// </summary>
public abstract class Abstract_Identity : MonoBehaviour {

    [SerializeField] protected Team_Attribute teamAttribute;
    [SerializeField] protected Abstract_Condition healthAttribute;
    [SerializeField] protected Movement movementAttribute;
    [SerializeField] protected Equipment equipment;

    [SerializeField] protected GameObject mainBody = null;

    [SerializeField] private UnityEvent OnKilledSomeOne = new UnityEvent();

    public static Abstract_Identity playerIdentity;
    
	// Use this for initialization
    void Start () {
        Assert.IsNotNull(mainBody);
        Assert.IsNotNull(teamAttribute);
        Assert.IsNotNull(healthAttribute);
        Assert.IsNotNull(movementAttribute);
        privCheckLayer();
        if (tag == "Player")
        {
            playerIdentity = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual public Identity GetIdentity()
    {
        return Identity.Not_Initialized;
    }

    public void Func_OnKilledSomeOne()
    {
        OnKilledSomeOne.Invoke();
    }

    void privCheckLayer()
    {
        string tmpLayerName = LayerMask.LayerToName(mainBody.layer);
        switch (tmpLayerName)
        {
            case CONSTANT.LAYER_NAME_ZOMBIE:
                Assert.IsTrue(teamAttribute.GetTeam() == Team.Zombie);
                break;
            case CONSTANT.LAYER_NAME_HUMAN:
                Assert.IsTrue(teamAttribute.GetTeam() == Team.Human);
                break;
            default:
                Assert.IsTrue(false);
                break;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
    // Upgrades
    public Movement getMovementComponent() { return movementAttribute; }
    public Abstract_Condition getConditionComponent() { return healthAttribute; }
    public Equipment getEquipmentComponent() { return equipment; }
    public Team_Attribute getTeamComponent() { return teamAttribute; }
}
