using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
    [SerializeField] protected Abstract_Movement movementAttribute;
    [SerializeField] protected Melee_Component meleeAttribute;

    [SerializeField] protected GameObject mainBody = null;

    
	// Use this for initialization
    void Start () {
        Assert.IsNotNull(mainBody);
        Assert.IsNotNull(teamAttribute);
        Assert.IsNotNull(healthAttribute);
        Assert.IsNotNull(movementAttribute);
        privCheckLayer();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual public Identity GetIdentity()
    {
        return Identity.Not_Initialized;
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

    abstract public void ChangeFaction(Identity newIdentity);
    abstract public void Die();
}
