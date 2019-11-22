using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Camera_Controller : MonoBehaviour {
    [SerializeField] private float camZoomMin = 5.0f;
    [SerializeField] private float camZoomMax = 30.0f;
    [SerializeField] private Transform targetObject;
    [SerializeField] private Transform camTran;
    [SerializeField] private Camera cam;

	// Use this for initialization
	void Start () {
        Assert.IsNotNull(targetObject);
        Assert.IsNotNull(camTran);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = targetObject.position;
        targetPos.z = camTran.position.z;
        camTran.position = targetPos;

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - Input.mouseScrollDelta.y, camZoomMin, camZoomMax);
        //cam.rotation = targetObject.rotation;
    }
}
