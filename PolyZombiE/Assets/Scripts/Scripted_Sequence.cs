﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scripted_Sequence : MonoBehaviour {
    [SerializeField] AI_StateMachine_Helper AI_Agent;
    [SerializeField] float distTolerance = 1.0f;
    [SerializeField] bool DestroyOnComplete = true;

    private Vector2 destination;
    bool inEffect = false;

    [SerializeField] UnityEvent OnReachDestination = new UnityEvent();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (inEffect)
        {
            if (AI_Agent != null)
            {
                // Move the AI
                AI_Agent.getOrient().lookAtStep(destination);
                AI_Agent.getAIMovement().Move(destination);

                // Check distance
                float distSqr = (destination - AI_Agent.getMovement().getPosition()).sqrMagnitude;
                if (distSqr < distTolerance * distTolerance)
                {
                    Func_OnReachDestination();
                }
            }
            else
            {
                Debug.LogWarning("Sequence in progress but agent missing!");
                inEffect = false;
            }
        }
	}

    public void moveToLocation(Transform destinationTransform)
    {
        inEffect = true;
        AI_Agent.GetComponent<Animator>().SetBool("Scripted", true);
        destination = destinationTransform.position;
    }

    private void Func_OnReachDestination()
    {
        OnReachDestination.Invoke();
        inEffect = false;

        if (DestroyOnComplete)
        {
            AI_Agent.GetComponent<Animator>().SetBool("Scripted", false);
            Destroy(gameObject);
        }
    }
}
