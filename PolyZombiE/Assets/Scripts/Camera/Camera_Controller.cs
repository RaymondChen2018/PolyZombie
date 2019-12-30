using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Camera_Controller : MonoBehaviour {
    [SerializeField] private float camZoomMin = 5.0f;
    [SerializeField] private float camZoomMax = 30.0f;
    [SerializeField] private Camera cam;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - Input.mouseScrollDelta.y, camZoomMin, camZoomMax);
    }
}
