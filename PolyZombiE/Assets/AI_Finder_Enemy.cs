using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
[System.Serializable]
public class memory
{
    public Transform enemy;
    public float timeRememberedLatest;
}
public class AI_Finder_Enemy : AI_Finder
{
    [SerializeField] private Team_Attribute teamAttribute;
    //[SerializeField] private Animator AI_StateMachine;
    [SerializeField] private List<memory> enemyInMemory;
    [SerializeField] private float memoryDurection = 10;
    [SerializeField] int memoryCapacity = 3;
    private Transform closestEnemyRemembered = null;

    [SerializeField] UnityEventInt OnRememberTargets = new UnityEventInt();

    // Use this for initialization
    void Start()
    {
        Assert.IsNotNull(teamAttribute);
        enemyInMemory = new List<memory>();
    }

    // Update is called once per frame
    protected override void UpdateDerived()
    {
        // Clean memory
        for (int i = 0; i < enemyInMemory.Count; i++)
        {
            memory mem = enemyInMemory[i];
            if (mem.enemy == null || mem.timeRememberedLatest + memoryDurection < Time.time)
            {
                enemyInMemory.RemoveAt(i);
                i--;
            }
        }

        // Call back
        OnRememberTargets.Invoke(enemyInMemory.Count);

        // Find closest enemy
        if (enemyInMemory.Count > 0)
        {
            Transform tmp = enemyInMemory[0].enemy.transform;
            Transform ret = tmp;
            float closestDistSqrTmp = ((Vector2)tmp.position - movement.getPosition()).sqrMagnitude;
            float closestDistSqr = closestDistSqrTmp;
            for (int i = 1; i < enemyInMemory.Count; i++)
            {
                tmp = enemyInMemory[i].enemy.transform;
                closestDistSqrTmp = ((Vector2)tmp.position - movement.getPosition()).sqrMagnitude;
                if (closestDistSqr > closestDistSqrTmp)
                {
                    closestDistSqr = closestDistSqrTmp;
                    ret = tmp;
                }
            }
            closestEnemyRemembered = ret;
        }
        else
        {
            closestEnemyRemembered = null;
        }
    }

    public void addToMemory(Transform newEnemy)
    {
        memory mem = enemyInMemory.Find(x => x.enemy == newEnemy);
        if (mem == null) { // New enemy
            mem = new memory();
            mem.enemy = newEnemy;
            mem.timeRememberedLatest = Time.time;
            enemyInMemory.Add(mem);
        }
        else
        {
            mem.timeRememberedLatest = Time.time;
        }

        // Too many
        while(enemyInMemory.Count > memoryCapacity)
        {
            enemyInMemory.RemoveAt(0);
        }
    }

    public Transform getClosestEnemy()
    {
        return closestEnemyRemembered;
    }
}
