﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Zombie_Identity : Abstract_Identity
{
    [Header("Zombie")]
    [Header("Stat")]
    /// <summary>
    /// Zombie with higher infected(ious)ness can infect human faster.
    /// </summary>
    [SerializeField] private float infectiousness = CONSTANT.MINIMUM_INFECTIOUSNESS;

    [Header("Output")]
    [SerializeField] private UnityEvent OnInfectedSomeOne = new UnityEvent();

    public override IDENTITY GetIdentity()
    {
        return IDENTITY.Zombie;
    }

    public void Func_OnInfectedSomeOne()
    {
        OnInfectedSomeOne.Invoke();
    }

    public float GetInfectiousness()
    {
        return infectiousness;
    }

    public void setInfectiousness(float value)
    {
        infectiousness = value;
    }
}
