using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public enum IDENTITY
{
    Not_Initialized,
    Human,
    Zombie
}

/// <summary>
/// Manager class of other faction attributes
/// </summary>
public abstract class Abstract_Identity : MonoBehaviour
{
    [Header("Pointers")]
    [SerializeField] protected Team_Attribute teamAttribute;
    [SerializeField] protected Abstract_Condition healthAttribute;
    [SerializeField] protected Movement movementAttribute;
    [SerializeField] protected Equipment equipment;

    [Header("Misc")]
    [SerializeField] protected GameObject mainBody = null;

    [Header("Output")]
    [SerializeField] private UnityEvent OnKilledSomeOne = new UnityEvent();
    [SerializeField] private UnityEvent OnSpawn = new UnityEvent();

    public static Abstract_Identity playerIdentity;

    void Awake()
    {
        OnSpawn.Invoke();
    }

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

    virtual public IDENTITY GetIdentity()
    {
        return IDENTITY.Not_Initialized;
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
                Assert.IsTrue(teamAttribute.GetTeam() == TEAM.Zombie);
                break;
            case CONSTANT.LAYER_NAME_HUMAN:
                Assert.IsTrue(teamAttribute.GetTeam() == TEAM.Human);
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
