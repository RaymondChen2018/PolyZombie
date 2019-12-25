using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Abstract_PlayerControl : MonoBehaviour {
    [SerializeField] private KeyCode keyUp;
    [SerializeField] private KeyCode keyDown;
    [SerializeField] private KeyCode keyLeft;
    [SerializeField] private KeyCode keyRight;

    [SerializeField] private KeyCode keyMelee;

    //[SerializeField] private Rigidbody2D RB;
    [SerializeField] protected PlayerDirectionFinder directionFinder;
    [SerializeField] protected Orient orientComponent;
    [SerializeField] protected Movement movementComponent;
    [SerializeField] protected Equipment equipment;
    [SerializeField] protected Team_Attribute teamComponent;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(directionFinder);
        Assert.IsNotNull(orientComponent);
        Assert.IsNotNull(movementComponent);
        Assert.IsNotNull(equipment);
        Assert.IsNotNull(teamComponent);
    }

    // Update is called once per frame
    protected virtual void Update () {
        // Orient
        orientComponent.lookAt((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // Movement
        Vector2 localMoveDir = Vector2.zero;
        if (Input.GetKey(keyUp))
        {
            localMoveDir += Vector2.up;
        }
        if (Input.GetKey(keyDown))
        {
            localMoveDir += Vector2.down;
        }
        if (Input.GetKey(keyLeft))
        {
            localMoveDir += Vector2.left;
        }
        if (Input.GetKey(keyRight))
        {
            localMoveDir += Vector2.right;
        }
        if(localMoveDir != Vector2.zero)// Normalize
        {
            localMoveDir.Normalize();
        }
        Vector2 worldMoveDir = directionFinder.AlignToGlobal(localMoveDir);
        movementComponent.Move(worldMoveDir);// Send movement to movement abstract

        // Melee
        if (Input.GetKeyDown(keyMelee))
        {
            equipment.initPrimaryAttack();
        }
    }
}
