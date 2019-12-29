using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Orient : MonoBehaviour {
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private float stepRatio = 0.3f;
    private Vector2 dof = Vector2.right;

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
    /// <param name="targetPos"></param>
    public void lookAtStep(Vector2 targetPos)
    {
        dof = targetPos - RB.position;
        float destAngle = Vector2.SignedAngle(Vector2.right, dof);
        RB.rotation = Mathf.LerpAngle(RB.rotation, destAngle, stepRatio);
    }

    public Vector2 GetDOF()
    {
        return transform.right;//dof.normalized;
    }
}
