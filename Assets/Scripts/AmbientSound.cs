using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    [SerializeField] float soundVolume;
    [SerializeField] float soundInterval;
    [SerializeField] float intervalVariance;
    AudioPlayer audioplayer;
    GameManager gm;
    void Start()
    {
        audioplayer = FindObjectOfType<AudioPlayer>();
        gm = FindObjectOfType<GameManager>();
        if (sounds.Length > 0) StartCoroutine(PlaySounds());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PlaySounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(soundInterval + Random.Range(0, intervalVariance));
            if (gm.playAmbient)
            {
                AudioClip sound = sounds[Random.Range(0, sounds.Length)];
                if (sound != null) audioplayer.PlayClip(sound, soundVolume);
            }
        }
    }
}
