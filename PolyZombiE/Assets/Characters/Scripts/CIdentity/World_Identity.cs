using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Identity : Abstract_Identity {
    public static World_Identity singleton;

    private void Awake()
    {
        singleton = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
