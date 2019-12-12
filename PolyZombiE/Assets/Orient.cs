using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Orient : MonoBehaviour {
    [SerializeField] Vector2 lookAtPos;
    [SerializeField] private Rigidbody2D RB;
    private Vector2 dof;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(RB);
	}
	
	// Update is called once per frame
	void Update () {
        dof = lookAtPos - RB.position;
        RB.rotation = Vector2.SignedAngle(Vector2.right, dof);
    }

    public void lookAt(Vector2 pos)
    {
        lookAtPos = pos;
    }

    public Vector2 GetDOF()
    {
        return dof.normalized;
    }
}
