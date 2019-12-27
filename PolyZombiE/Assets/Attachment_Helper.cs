using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Attachment_Helper : MonoBehaviour {
    [SerializeField] Transform parent = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        privUpdateTransform();
    }

    private void privUpdateTransform()
    {
        if(parent != null)
        {
            
            transform.position = parent.position;
            transform.rotation = parent.rotation;
        }
    }

    void OnDrawGizmos()
    {
        privUpdateTransform();
    }

    public void SetAttachment(Transform newParent)
    {
        parent = newParent;
    }
}
