using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Logic_Timer : MonoBehaviour {
    [SerializeField] float duration = 1.0f;
    [SerializeField] bool enable = false;
    [SerializeField] bool autoRepeat = false;
    private float timeSinceEnabled = 0;

    [SerializeField] UnityEvent OnTimer = new UnityEvent();
    [SerializeField] UnityEvent OnEnabled = new UnityEvent();

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {
		if(enable && Time.time > timeSinceEnabled + duration)
        {
            OnTimer.Invoke();
            enable = false;

            // Auto repeat
            if (autoRepeat)
            {
                Enable();
            }
        }
	}

    public void Enable()
    {
        OnEnabled.Invoke();
        enable = true;
        timeSinceEnabled = Time.time;
    }
}
