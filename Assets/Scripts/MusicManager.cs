using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance;

    public AudioSource audioSource;

    public AudioClip background, match, miss, move, start;


    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void PlayBackground() {
        audioSource.clip = background;
        audioSource.Play();
    }

    public void PlayMatch() {
        audioSource.clip = match;
        audioSource.Play();
    }

    public void PlayMiss() {
        audioSource.clip = miss;
        audioSource.Play();
    }  
    public void PlayMove() {

        audioSource.clip = move;
        audioSource.Play();
    
    }  
    public void PlayStart() {
        audioSource.clip = start;
        audioSource.Play();
    }

}
