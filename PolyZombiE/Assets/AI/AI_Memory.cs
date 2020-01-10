using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[System.Serializable]
public class Memory
{
    public Transform enemy;
    public Vector2 lastSeenPosition;
    public float timeRememberedLatest;
}

public class AI_Memory: MonoBehaviour {
    [Tooltip("Does memory remove entries of same identity (eg. sound source has no identity, should be false)")][SerializeField] private bool uniqueMemory = true;
    [SerializeField] private List<Memory> enemyInMemory;
    [SerializeField] private float memoryDurection = 10;
    [SerializeField] int memoryCapacity = 3;

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
            // Target is missing (eg. Dead)
            if (uniqueMemory && mem.enemy == null)
            {
                enemyInMemory.RemoveAt(i);
                i--;
            }
            // Memory expired
            else if (mem.timeRememberedLatest + memoryDurection < Time.time)
            {
                OnForgetTarget.Invoke(mem.enemy);
                enemyInMemory.RemoveAt(i);
                i--;
            }
        }

        // Call back
        //Debug.Log("memory mem: " + enemyInMemory.Count);
        OnRememberTargets.Invoke(enemyInMemory.Count);
    }

    public void clearMemory()
    {
        enemyInMemory.Clear();
        OnRememberTargets.Invoke(0);
    }

    public void addToMemory(Vector2 newLocation)
    {
        if (uniqueMemory)
        {
            Debug.LogWarning("You are adding entry without unique id, but ask for unique memory!");
        }

        Memory mem = new Memory();
        mem.enemy = null;
        mem.lastSeenPosition = newLocation;
        mem.timeRememberedLatest = Time.time;
        enemyInMemory.Add(mem);

        // Too many
        while (enemyInMemory.Count > memoryCapacity)
        {
            enemyInMemory.RemoveAt(0);
        }
    }

    public void addToMemory(Transform newEnemy)
    {
        Memory mem = enemyInMemory.Find(x => x.enemy == newEnemy);
        if (mem == null) // New enemy
        { 
            mem = new Memory();
            mem.enemy = newEnemy;
            mem.lastSeenPosition = newEnemy.transform.position;
            mem.timeRememberedLatest = Time.time;
            enemyInMemory.Add(mem);
        }
        else
        {
            mem.lastSeenPosition = newEnemy.transform.position;
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

    public List<Memory> getMemoryCache()
    {
        for (int i = 0; i < enemyInMemory.Count; i++)
        {
            if (enemyInMemory[i] == null)
            {
                enemyInMemory.RemoveAt(i);
                i--;
            }
        }
        return enemyInMemory;
    }
}
