using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Orient : MonoBehaviour {
    [SerializeField] private Rigidbody2D RB;
    private Vector2 dof;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(RB);
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    /// <summary>
    /// For player controller use
    /// </summary>
    /// <param name="pos"></param>
    public void lookAt(Vector2 pos)
    {
        //lookAtPos = pos;
        dof = pos - RB.position;
        float destAngle = Vector2.SignedAngle(Vector2.right, dof);
        RB.rotation = destAngle;
    }

    /// <summary>
    /// For AI controller use
    /// </summary>
    /// <param name="pos"></param>
    public void lookAtAI(Vector2 pos)
    {
        dof = pos - RB.position;
        float destAngle = Vector2.SignedAngle(Vector2.right, dof);
        RB.rotation = Mathf.LerpAngle(RB.rotation, destAngle, 0.3f);
    }

    public Vector2 GetDOF()
    {
        return dof.normalized;
    }
}
