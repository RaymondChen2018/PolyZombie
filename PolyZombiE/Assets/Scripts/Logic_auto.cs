using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Logic_auto : MonoBehaviour {
    [SerializeField] private UnityEvent OnMapSpawn = new UnityEvent();
    [SerializeField] private UnityEvent OnLevelStart = new UnityEvent();

    // Use this for initialization
    void Start () {
        OnMapSpawn.Invoke();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void levelStart()
    {
        OnLevelStart.Invoke();
    }
}
