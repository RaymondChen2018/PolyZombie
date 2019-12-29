using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventBool : UnityEvent<bool>
{

}
public class AI_Sense_Pain : MonoBehaviour
{
    [SerializeField] private float painLastDuration = 2;
    private bool feelPain = false;
    private float timeLastPain = 0;
    /// <summary>
    /// From activator to victim
    /// </summary>
    private Vector2 painDirectionLastest;
    private float painAmountLatest = 0;

    [SerializeField] UnityEvent OnFeelPain = new UnityEvent();
    [SerializeField] UnityEvent OnPainReceded = new UnityEvent();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > timeLastPain + painLastDuration && feelPain)
        {
            feelPain = false;
            OnPainReceded.Invoke();
        }
	}

    public void receivePain(DamageInfo damageInfo)
    {
        feelPain = true;
        painDirectionLastest = damageInfo.direction;
        painAmountLatest = damageInfo.damageAmount;
        timeLastPain = Time.time;
        OnFeelPain.Invoke();
    }

    /// <summary>
    /// From activator to victim
    /// </summary>
    public Vector2 getLatestDamageDirection()
    {
        return painDirectionLastest;
    }

    public float getLatestDamageAmount()
    {
        return painAmountLatest;
    }
}
