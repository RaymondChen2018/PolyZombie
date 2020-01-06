using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AI_State_Combat_Melee : StateMachineBehaviour {

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
        AI_Finder aiEnemyFinder = helper.getEnemyFinder();
        Vector2 thisPos = movement.getPosition();

        List<Transform> memoryCache = aiEnemyFinder.getSightCache();
        if(memoryCache.Count == 0)
        {
            Debug.LogWarning("attack target not found");
            return;
        }
        
        // Get closest
        Vector2 tmp = memoryCache[0].position;
        Vector2 closestPos = tmp;
        float closestDistSqrTmp = (tmp - thisPos).sqrMagnitude;
        float closestDistSqr = closestDistSqrTmp;
        for (int i = 1; i < memoryCache.Count; i++)
        {
            tmp = memoryCache[i].position;
            closestDistSqrTmp = (tmp - thisPos).sqrMagnitude;
            if (closestDistSqr > closestDistSqrTmp)
            {
                closestDistSqr = closestDistSqrTmp;
                closestPos = tmp;
            }
        }

        // Engage
        orient.lookAtStep(closestPos);
        aiMovement.Move(closestPos);

        // Attack when close
        AI_Weapon_Helper weaponAIHelper = equipment.getWeapon().GetComponent<AI_Weapon_Helper>();
        Assert.IsNotNull(weaponAIHelper);
        float meleeRange = weaponAIHelper.getAttackRange();
        float enemyDistSqr = (closestPos - thisPos).sqrMagnitude;

        if (enemyDistSqr < meleeRange * meleeRange)
        {
            equipment.initPrimaryAttack();
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
