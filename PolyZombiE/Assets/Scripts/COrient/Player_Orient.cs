using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Orient : MonoBehaviour {
    [SerializeField] private Rigidbody2D RB;
    private Vector2 currMousePos;
    private Vector2 prevMousePos;

    [SerializeField] private float spriteAngleOffset;
    private Vector2 dof;
	// Use this for initialization
	void Start () {
        prevMousePos = Input.mousePosition;
    }
	
	// Update is called once per frame
	void Update () {
        dof = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - RB.position;
        RB.rotation = Vector2.SignedAngle(Vector2.right, dof) - spriteAngleOffset;
    }

    public Vector2 GetDOF()
    {
        return dof.normalized;
    }
}
