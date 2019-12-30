using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventVector2 : UnityEvent<Vector2>
{

}

public class AI_Sense_Sound : MonoBehaviour {

    [SerializeField] private UnityEventVector2 OnHearSound = new UnityEventVector2();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ringSound(Vector2 soundSourcePos)
    {
        OnHearSound.Invoke(soundSourcePos);
    }
}
