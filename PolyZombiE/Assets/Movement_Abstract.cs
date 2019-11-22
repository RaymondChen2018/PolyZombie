using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement_Abstract : MonoBehaviour {

    [SerializeField] private float movementSpeed;
    [SerializeField] private Rigidbody2D RB;

    protected Vector2 moveDirVector;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RB.velocity = moveDirVector.normalized * movementSpeed;
    }
}
