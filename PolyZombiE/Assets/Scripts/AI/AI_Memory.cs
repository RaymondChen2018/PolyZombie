using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[System.Serializable]
public class Memory
{
    public Transform enemy;
    public float timeRememberedLatest;
}

public class AI_Memory: MonoBehaviour {
    [SerializeField] private List<Memory> enemyInMemory;
    [SerializeField] private float memoryDurection = 10;
    [SerializeField] int memoryCapacity = 3;
    private Transform closestEnemyRemembered = null;
    [SerializeField] UnityEventInt OnRememberTargets = new UnityEventInt();
    [SerializeField] UnityEventTranform OnForgetTarget = new UnityEventTranform();

    // Use this for initialization
    void Start()
    {
        enemyInMemory = new List<Memory>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 thisPos = transform.position;

        // Clean memory
        for (int i = 0; i < enemyInMemory.Count; i++)
        {
            Memory mem = enemyInMemory[i];
            if (mem.enemy == null)
            {
                enemyInMemory.RemoveAt(i);
                i--;
            }
            else if (mem.timeRememberedLatest + memoryDurection < Time.time)
            {
                OnForgetTarget.Invoke(mem.enemy);
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
            float closestDistSqrTmp = ((Vector2)tmp.position - thisPos).sqrMagnitude;
            float closestDistSqr = closestDistSqrTmp;
            for (int i = 1; i < enemyInMemory.Count; i++)
            {
                tmp = enemyInMemory[i].enemy.transform;
                closestDistSqrTmp = ((Vector2)tmp.position - thisPos).sqrMagnitude;
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
        Memory mem = enemyInMemory.Find(x => x.enemy == newEnemy);
        if (mem == null)
        { // New enemy
            mem = new Memory();
            mem.enemy = newEnemy;
            mem.timeRememberedLatest = Time.time;
            enemyInMemory.Add(mem);
        }
        else
        {
            mem.timeRememberedLatest = Time.time;

            // Prioritize
            enemyInMemory.Remove(mem);
            enemyInMemory.Add(mem);
        }

        // Too many
        while (enemyInMemory.Count > memoryCapacity)
        {
            enemyInMemory.RemoveAt(0);
        }
    }
    public Transform getClosestEnemy()
    {
        return closestEnemyRemembered;
    }
}
