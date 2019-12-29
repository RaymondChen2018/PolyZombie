using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_State_Alert_Pain : StateMachineBehaviour {

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
        Vector2 thisPos = movement.getPosition();
        
        bool feelPain = animator.GetBool("FeelPain");
        //List<Vector2> locationMemory = helper.getMemory().getLocations();
        if (feelPain)
        {
            AI_Sense_Pain painRecepter = helper.getPainRecepter();
            Vector2 painDirection = painRecepter.getLatestDamageDirection();
            Vector2 respondToDir = thisPos + painDirection;
            orient.lookAtStep(respondToDir);
        }
        //else if(locationMemory.Count > 0)
        //{
        //    Vector2 targetLocation = locationMemory[locationMemory.Count - 1];
        //    orient.lookAtStep(targetLocation);

        //    //bool weaponized = 
        //    //movement.Move(targetLocation - thisPos);
        //}
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
