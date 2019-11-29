using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

/// <summary>
/// Create callback of and when character dies or gets infected etc.
/// </summary>
public class Math_Counter : MonoBehaviour {
    [SerializeField] private int maxValue = 1;
    [SerializeField] private int minValue = 0;
    [SerializeField] private int value = 1;
    private int prevValue;
    [SerializeField] private UnityEvent OnHitMin = new UnityEvent();
    [SerializeField] private UnityEvent OnHitMax = new UnityEvent();
    [SerializeField] private UnityEvent OnValueChanged = new UnityEvent();

    
    // Use this for initialization
    void Start () {
        prevValue = value;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Add(int number)
    {
        value = Mathf.Clamp(value + number, minValue, maxValue);
        if(value != prevValue)
        {
            OnValueChanged.Invoke();
            if (value == maxValue)
            {
                OnHitMax.Invoke();
            }
            if(value == minValue)
            {
                OnHitMin.Invoke();
            }
        }

        prevValue = value;
    }
    public void Subtract(int number)
    {
        Add(-number);
    }
}
