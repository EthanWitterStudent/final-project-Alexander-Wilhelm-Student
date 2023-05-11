using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// You have no clue how long it took me to get this to work with the persistence between loads.

public class MusicLoop : MonoBehaviour
{
    public AudioClip LoopMusic;
    AudioSource src;
    public bool looping;

    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!src.isPlaying && !src.loop && looping) { //what the FUCK. am i doing
            src.clip = LoopMusic;
            src.loop = true;
            src.Play();
        }

    }

    
}
