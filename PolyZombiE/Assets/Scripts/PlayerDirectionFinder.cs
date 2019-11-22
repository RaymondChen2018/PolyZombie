using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerDirectionFinder : MonoBehaviour {
    public enum AlignWith
    {
        GlobalForward,
        RBRotation
    }

    private Vector2 forward = Vector2.up;
    private Vector2 backward = Vector2.down;
    private Vector2 leftward = Vector2.left;
    private Vector2 rightward = Vector2.right;

    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private AlignWith alignType = AlignWith.GlobalForward;

    // Use this for initialization
    void Start () {
        Assert.AreNotEqual(RB, null);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector2 AlignToGlobal(Vector2 localDirection)
    {
        float angleOffset = 0.0f;
        switch (alignType)
        {
            case AlignWith.GlobalForward:
                angleOffset = 0.0f;
                break;
            case AlignWith.RBRotation:
                angleOffset = RB.rotation;
                break;
        }
        return Quaternion.AngleAxis(angleOffset, Vector3.forward) * localDirection;
    }
}
