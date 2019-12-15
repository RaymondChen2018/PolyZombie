using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Logic_auto : MonoBehaviour {
    [SerializeField] private UnityEvent OnMapSpawn = new UnityEvent();

    void Awake()
    {
        OnMapSpawn.Invoke();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
