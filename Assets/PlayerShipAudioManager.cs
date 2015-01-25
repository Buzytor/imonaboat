using UnityEngine;
using System.Collections;

public class PlayerShipAudioManager : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip hornEnd;
    public AudioClip hornLoop;

    

    internal void StartHorn() {
        audioSource.clip = hornLoop;
        audioSource.Play();
        audioSource.loop = true;
    }

    internal void StopHorn() {
        audioSource.Stop();
        audioSource.PlayOneShot(hornEnd);
    }
}
