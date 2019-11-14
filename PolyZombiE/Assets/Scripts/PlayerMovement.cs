using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private KeyCode moveUp;
    [SerializeField] private KeyCode moveDown;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode moveRight;

    /// <summary>
    /// Units/second
    /// </summary>
    [SerializeField] private float movementSpeed;

    [SerializeField] private Rigidbody2D RB;

    // Use this for initialization
    void Start () {
        Assert.AreNotEqual(RB, null);
	}

    // Update is called once per frame
    void Update () {
        // Compose move direction
        Vector2 moveDir = Vector2.zero;
        if (Input.GetKey(moveUp))
        {
            moveDir += Vector2.up;
        }
        if (Input.GetKey(moveDown))
        {
            moveDir += Vector2.down;
        }
        if (Input.GetKey(moveLeft))
        {
            moveDir += Vector2.left;
        }
        if (Input.GetKey(moveRight))
        {
            moveDir += Vector2.right;
        }

        // Normalize
        if(moveDir != Vector2.zero)
        {
            moveDir.Normalize();
        }

        // Compose force
        moveDir *= movementSpeed * Time.deltaTime;

        // Push rigidbody
        RB.velocity = moveDir;
    }
}
