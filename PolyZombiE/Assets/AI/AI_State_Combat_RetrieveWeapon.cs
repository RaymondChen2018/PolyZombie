using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_State_Combat_RetrieveWeapon : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AI_StateMachine_Helper helper = animator.GetComponent<AI_StateMachine_Helper>();
        Movement movement = helper.getMovement();
        AI_Movement aiMovement = helper.getAIMovement();
        Orient orient = helper.getOrient();
        Equipment equipment = helper.getEquipment();
        AI_Finder weaponFinder = helper.getWeaponFinder();

        List<Transform> sightCache = weaponFinder.getSightCache();
        if (sightCache.Count == 0)
        {
            Debug.LogWarning("Weapon not found");
            return;
        }

        // Find closest weapon
        Vector2 thisPos = movement.getPosition();
        Transform tmp = sightCache[0];
        Transform weaponClosest = tmp;
        float closestDistSqrTmp = ((Vector2)tmp.position - thisPos).sqrMagnitude;
        float closestDistSqr = closestDistSqrTmp;
        for (int i = 1; i < sightCache.Count; i++)
        {
            tmp = sightCache[i];
            closestDistSqrTmp = ((Vector2)tmp.position - thisPos).sqrMagnitude;
            if (closestDistSqr > closestDistSqrTmp)
            {
                closestDistSqr = closestDistSqrTmp;
                weaponClosest = tmp;
            }
        }

        // Move & Face enemy
        Vector2 enemyPos = weaponClosest.position;
        orient.lookAtStep(enemyPos);
        aiMovement.Move(enemyPos);

        // Fetch when close
        float pickupRadius = equipment.getPickUpRadius();
        float enemyDistSqr = (enemyPos - thisPos).sqrMagnitude;

        if (enemyDistSqr < pickupRadius * pickupRadius)
        {
            equipment.pickUp();
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
