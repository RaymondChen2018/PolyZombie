using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Rigidbody2D RB;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(RB);
        Assert.IsTrue(movementSpeed > 0.0f);
    }
	
	// Update is called once per frame
	void Update () {

    }

    /// <summary>
    /// If magnitude larger than 0.0f, then moving
    /// </summary>
    /// <param name="dir"></param>
    public void Move(Vector2 dir)
    {
        RB.AddForce(dir.normalized * movementSpeed * Time.deltaTime);
    }

    public Vector2 getPosition()
    {
        return RB.position;
    }
}
