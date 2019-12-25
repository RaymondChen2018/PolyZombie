using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventInt : UnityEvent<int>
{
}
[System.Serializable]
public class UnityEventString : UnityEvent<string>
{
}
/// <summary>
/// Create callback of and when character dies or gets infected etc.
/// </summary>
public class Math_Counter : MonoBehaviour {
    [Header("Stat")]
    [SerializeField] private int maxValue = 1;
    [SerializeField] private int minValue = 0;
    [SerializeField] private int value = 1;
    private int prevValue;

    [Header("Output")]
    [SerializeField] private UnityEvent OnHitMin = new UnityEvent();
    [SerializeField] private UnityEvent OnHitMax = new UnityEvent();
    [SerializeField] private UnityEvent OutValue = new UnityEvent();
    [SerializeField] private UnityEvent OnAddValue = new UnityEvent();
    [SerializeField] private UnityEvent OnSubtractValue = new UnityEvent();
    [SerializeField] private UnityEventInt OnValueChanged = new UnityEventInt();

    // Use this for initialization
    void Start () {
        prevValue = value;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Add(int number)
    {
        OnAddValue.Invoke();
        setValue(value + number);
    }

    public void Subtract(int number)
    {
        OnSubtractValue.Invoke();
        setValue(value - number);
    }

    public int getValue()
    {
        return value;
    }

    public void setValue(int number)
    {
        value = Mathf.Clamp(number, minValue, maxValue);
        if (value != prevValue)
        {
            OutValue.Invoke();
            OnValueChanged.Invoke(value);
            if (value == maxValue)
            {
                OnHitMax.Invoke();
            }
            if (value == minValue)
            {
                OnHitMin.Invoke();
            }
            prevValue = value;
        }
    }
    public void setMaxValue(int newMax)
    {
        maxValue = newMax;
    }
    public void setMinValue(int newMin)
    {
        minValue = newMin;
    }
}
