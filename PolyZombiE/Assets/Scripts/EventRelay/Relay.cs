using UnityEngine.Events;

[System.Serializable]
public class Relay
{
    public UnityEvent eventTrigger = new UnityEvent();
    public bool triggerOnce = false;

    public void Invoke()
    {
        eventTrigger.Invoke();
        if (triggerOnce)
        {
            eventTrigger.RemoveAllListeners();
        }
    }
}