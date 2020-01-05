using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_State_Alert_ResumeCombat : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AI_StateMachine_Helper helper = animator.GetComponent<AI_StateMachine_Helper>();
        Movement movement = helper.getMovement();
        Orient orient = helper.getOrient();
        AI_Memory aiMemory = helper.getMemory();
        Vector2 thisPos = movement.getPosition();

        List<Memory> memoryCache = aiMemory.getMemoryCache();
        if (memoryCache.Count == 0)
        {
            Debug.LogWarning("flee target not found");
            return;
        }

        // Go to last combat location
        Vector2 lastSeenEnemyPos = memoryCache[0].lastSeenPosition;
        Vector2 moveDir = lastSeenEnemyPos - thisPos;
        orient.lookAtStep(thisPos + moveDir);
        movement.Move(moveDir);

        // Combat point reach
        bool positionReached = movement.positionReached(lastSeenEnemyPos);
        if (positionReached)
        {
            memoryCache.RemoveAt(0);
            animator.SetTrigger("CombatPointReached");
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
