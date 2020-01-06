using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Trigger_Volume : MonoBehaviour {
    [SerializeField] string filterTag = "";
    [SerializeField] UnityEvent OnStartTouch = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(filterTag == "" || filterTag == collision.tag)
        {
            OnStartTouch.Invoke();
        }
    }
}
