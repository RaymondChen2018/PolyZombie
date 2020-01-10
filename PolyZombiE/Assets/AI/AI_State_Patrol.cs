using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_State_Patrol : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int totalPatrolSpotCount = Navigation_manual.getPatrolLocationCount();
        int patrolIndex = -1;
        if (totalPatrolSpotCount > 0)
        {
            patrolIndex = UnityEngine.Random.Range(0, totalPatrolSpotCount - 1);
        }
        
        animator.SetInteger("PatrolLocationIndex", patrolIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AI_StateMachine_Helper helper = animator.GetComponent<AI_StateMachine_Helper>();
        Movement movement = helper.getMovement();
        AI_Movement aiMovement = helper.getAIMovement();
        Orient orient = helper.getOrient();
        int patrolIndex = animator.GetInteger("PatrolLocationIndex");

        // Patrol if location available
        if(patrolIndex > 0)
        {
            Transform randomPatrolSpot = Navigation_manual.getPatrolLocation(patrolIndex);
            Vector2 visitPos = randomPatrolSpot.position;
            orient.lookAtStep(visitPos);
            aiMovement.Move(visitPos);

            // Patrol point reach
            bool positionReached = movement.positionReached(visitPos);
            if (positionReached)
            {
                animator.SetTrigger("PatrolPointReached");
            }
        }
        else
        {
            Debug.LogWarning("Patrol location not found");
            animator.SetTrigger("PatrolPointReached");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.SetBool("isPatrolling", false);
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
