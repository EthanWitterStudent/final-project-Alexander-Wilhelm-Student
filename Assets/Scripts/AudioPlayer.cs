using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    AudioSource src;

    void Start() {
        src = gameObject.GetComponent<AudioSource>();
        if (src != null) Debug.Log("audiosrc exists"); else Debug.Log("shit");
    }

    public void PlayClip(AudioClip clip, float volume) //im sorry gary but whatever you did can only be described as "fucking ridiculous" and i hereby fire you from ever working in audio engineering ever again
    {
        if(clip != null)
        {
            src.PlayOneShot(clip, volume);
        }
    }
}
