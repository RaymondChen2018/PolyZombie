using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_State_Flee : StateMachineBehaviour {


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    Vector2 enemyPos = aiEnemyFinder.getClosestEnemy().position;
    //    Vector2 thisPos = movement.getPosition();
    //    movement.SetDirectionVector(thisPos - enemyPos);
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AI_StateMachine_Helper helper = animator.GetComponent<AI_StateMachine_Helper>();
        Movement movement = helper.getMovement();
        AI_Movement aiMovement = helper.getAIMovement();
        Orient orient = helper.getOrient();
        AI_Memory aiMemory = helper.getMemory();
        Vector2 thisPos = movement.getPosition();

        List<Memory> memoryCache = aiMemory.getMemoryCache();
        if (memoryCache.Count == 0)
        {
            Debug.LogWarning("flee target not found");
            return;
        }

        // Get closest
        Vector2 tmp = memoryCache[0].lastSeenPosition;
        Vector2 enemyPos = tmp;
        float closestDistSqrTmp = (tmp - thisPos).sqrMagnitude;
        float closestDistSqr = closestDistSqrTmp;
        for (int i = 1; i < memoryCache.Count; i++)
        {
            tmp = memoryCache[i].lastSeenPosition;
            closestDistSqrTmp = (tmp - thisPos).sqrMagnitude;
            if (closestDistSqr > closestDistSqrTmp)
            {
                closestDistSqr = closestDistSqrTmp;
                enemyPos = tmp;
            }
        }

        // Flee from
        Vector2 moveDir = thisPos - enemyPos;
        orient.lookAtStep(thisPos + moveDir);
        movement.Move(moveDir);
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
