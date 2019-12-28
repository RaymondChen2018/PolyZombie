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
    [SerializeField] AI_Finder_Enemy aiEnemyFinder;
    [SerializeField] AI_Finder aiWeaponFinder;

    [SerializeField] bool aiCombative = false;

    Animator animator;

    // Use this for initialization
    void Start()
    {
        Assert.IsNotNull(orient);
        Assert.IsNotNull(movement);
        Assert.IsNotNull(equipment);
        Assert.IsNotNull(teamAttribute);
        Assert.IsNotNull(aiEnemyFinder);

        // Getcomponent to verify this object is parallel with an animator at the same time
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);

        animator.SetBool("Combative", aiCombative);
    }

    public Orient getOrient()
    {
        return orient;
    }
    public Movement getMovement()
    {
        return movement;
    }
    public AI_Finder_Enemy getEnemyFinder()
    {
        return aiEnemyFinder;
    }

    public AI_Finder getWeaponFinder()
    {
        return aiWeaponFinder;
    }

    public Equipment getEquipment()
    {
        return equipment;
    }

    internal Team_Attribute getTeamAttribute()
    {
        return teamAttribute;
    }

    public void updateNearbyWeaponCount(int param)
    {
        animator.SetInteger("WeaponInSight", param);
    }
    public void updateEnemyInSight(int param)
    {
        animator.SetInteger("EnemyInSight", param);
    }
    public void updateEnemyInMemory(int param)
    {
        animator.SetInteger("EnemyRemembered", param);
    }
}
