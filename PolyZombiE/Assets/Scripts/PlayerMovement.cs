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
    [SerializeField] private PlayerDirectionFinder directionFinder;


    // Use this for initialization
    void Start () {
        Assert.AreNotEqual(RB, null);
        Assert.AreNotEqual(directionFinder, null);
    }

    // Update is called once per frame
    void Update () {
        // Compose move direction
        Vector2 localMoveDir = Vector2.zero;
        if (Input.GetKey(moveUp))
        {
            localMoveDir += Vector2.up;
        }
        if (Input.GetKey(moveDown))
        {
            localMoveDir += Vector2.down;
        }
        if (Input.GetKey(moveLeft))
        {
            localMoveDir += Vector2.left;
        }
        if (Input.GetKey(moveRight))
        {
            localMoveDir += Vector2.right;
        }

        // Normalize
        if(localMoveDir != Vector2.zero)
        {
            localMoveDir.Normalize();
        }

        // Compose force
        
        localMoveDir *= movementSpeed * Time.deltaTime;

        // Push rigidbody
        Vector2 worldMoveDir = directionFinder.AlignToGlobal(localMoveDir);
        RB.velocity = worldMoveDir;
    }
}
