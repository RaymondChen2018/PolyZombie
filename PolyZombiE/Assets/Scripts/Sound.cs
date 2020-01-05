using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
    [SerializeField] private float soundTravelDistance = 100.0f;
    [SerializeField] private LayerMask aiSoundEarMask;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void makeSound()
    {
        Vector2 soundSourcePosition = transform.position;
        Collider2D[] ears = Physics2D.OverlapCircleAll(soundSourcePosition, soundTravelDistance, aiSoundEarMask);
        for (int i = 0; i < ears.Length; i++)
        {
            AI_Sense_Sound aiEar = ears[i].GetComponent<AI_Sense_Sound>();
            aiEar.ringSound(soundSourcePosition);
        }
    }
}
