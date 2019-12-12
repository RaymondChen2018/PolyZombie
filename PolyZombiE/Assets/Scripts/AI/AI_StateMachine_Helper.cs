using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AI_StateMachine_Helper : MonoBehaviour
{
    [SerializeField] Orient orient;
    [SerializeField] Movement movement;
    [SerializeField] Equipment equipment;
    [SerializeField] Team_Attribute teamAttribute;
    [SerializeField] AI_EnemyFinder aiEnemyFinder;

    // Use this for initialization
    void Start()
    {
        Assert.IsNotNull(orient);
        Assert.IsNotNull(movement);
        Assert.IsNotNull(equipment);
        Assert.IsNotNull(teamAttribute);
        Assert.IsNotNull(aiEnemyFinder);
    }

    public Orient getOrient()
    {
        return orient;
    }
    public Movement getMovement()
    {
        return movement;
    }
    public AI_EnemyFinder getEnemyFinder()
    {
        return aiEnemyFinder;
    }
    public Equipment getEquipment()
    {
        return equipment;
    }

    internal Team_Attribute getTeamAttribute()
    {
        return teamAttribute;
    }
}
