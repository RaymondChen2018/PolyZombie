using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_EnemyMemory : MonoBehaviour {
    List<Collider2D> enemyInMemory;

	// Use this for initialization
	void Start () {
        enemyInMemory = new List<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
