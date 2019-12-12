using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Movement : MonoBehaviour {

    [SerializeField] private float movementSpeed;
    [SerializeField] private Rigidbody2D RB;

    protected Vector2 moveDirVector;
    // Use this for initialization
    void Start () {
        Assert.IsNotNull(RB);
        Assert.IsTrue(movementSpeed > 0.0f);
        moveDirVector = Vector2.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if(moveDirVector.sqrMagnitude != 0.0f)
        {
            RB.AddForce(moveDirVector.normalized * movementSpeed * Time.deltaTime);
            //RB.velocity = moveDirVector.normalized * movementSpeed * 0.01f;//Time.deltaTime;
        }
        else
        {
            //RB.velocity = Vector2.zero;
        }
        moveDirVector = Vector2.zero;
    }

    /// <summary>
    /// If magnitude larger than 0.0f, then moving
    /// </summary>
    /// <param name="dir"></param>
    public void SetDirectionVector(Vector2 dir)
    {
        moveDirVector = dir;
    }

    public Vector2 getPosition()
    {
        return RB.position;
    }
}
