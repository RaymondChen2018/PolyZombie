using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Trigger_Volume : MonoBehaviour {
    [SerializeField] UnityEvent OnStartTouch = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnStartTouch.Invoke();
    }
}
