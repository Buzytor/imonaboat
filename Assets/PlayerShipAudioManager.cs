using UnityEngine;
using System.Collections;

public class PlayerShipAudioManager : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip hornStart;
    public AudioClip hornEnd;
    public AudioClip hornLoop;

    

    internal void StartHorn() {
        audioSource.Stop();
        audioSource.clip = hornLoop;
        audioSource.PlayDelayed(hornStart.length);
        audioSource.PlayOneShot(hornStart);
        audioSource.loop = true;
    }

    internal void StopHorn() {
        audioSource.clip = null;
        audioSource.Stop();
        audioSource.PlayOneShot(hornEnd);
    }
}
