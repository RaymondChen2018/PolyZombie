﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_State_Combat_Range : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("combat range!");
        AI_StateMachine_Helper helper = animator.GetComponent<AI_StateMachine_Helper>();
        Orient orient = helper.getOrient();
        AI_EnemyFinder aiEnemyFinder = helper.getEnemyFinder();

        Transform enemyTransform = aiEnemyFinder.getClosestEnemy();
        if (enemyTransform != null)
        {
            Vector2 enemyPos = enemyTransform.position;
            orient.lookAtAI(enemyPos);
        }
        else
        {
            Debug.LogWarning("closest enemy null!");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}