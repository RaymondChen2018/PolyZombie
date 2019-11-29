using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commemtary : MonoBehaviour {
    [SerializeField] AudioSource audioPlayer;

    public void Play(AudioClip audio)
    {
        audioPlayer.Stop();
        audioPlayer.clip = audio;
        audioPlayer.Play();
    }
}
