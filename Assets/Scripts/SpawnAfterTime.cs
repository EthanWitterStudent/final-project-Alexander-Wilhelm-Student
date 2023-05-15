using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAfterTime : MonoBehaviour //pants shitted
{
    AudioPlayer audioPlayer;
    [SerializeField] GameObject spawnObject;
    [SerializeField] AudioClip spawnSound;
    [SerializeField] float soundVolume;
    [SerializeField] float SpawnDelay;
    [SerializeField] float SpawnDelayVariance;
    [SerializeField] float postSoundDelay;
    [SerializeField] bool destroyOnSpawn;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        float waitTime = SpawnDelay + Random.Range(0, SpawnDelayVariance);
        Invoke("Sound", waitTime);
    }

    void Sound() {
        audioPlayer.PlayClip(spawnSound, soundVolume);
        Invoke("Spawn", postSoundDelay);
    }

    void Spawn() {
        Instantiate(spawnObject, transform.position, Quaternion.identity);
        if (destroyOnSpawn) Destroy(gameObject);
    }
}
