using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment_Helper : MonoBehaviour {
    [SerializeField] Transform parent = null;
    [SerializeField] private bool fixRotation = false;
    [SerializeField] private bool fixZ = false;

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
            Vector3 tmp = parent.position;
            if (fixZ)
            {
                tmp.z = transform.position.z;
            }
            transform.position = tmp;
            if (!fixRotation)
            {
                transform.rotation = parent.rotation;
            }
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
