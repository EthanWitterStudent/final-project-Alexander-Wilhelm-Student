using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // big chungus
    [SerializeField] int health = 50;
    [SerializeField] bool player;
    [SerializeField] float flashLength = 0.025f;
    [SerializeField] int cashOnDeath;

    [SerializeField] GameObject deathEffect;

    AudioPlayer audioPlayer;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] float hitVolume = 1;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float deathVolume = 1;
    float flashTimer;
    Material flash;

    CameraShake shake;

    [SerializeField] float hitShakeAmount;
    [SerializeField] float deathShakeAmount;
    [SerializeField] float ShakeDecay;


    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        flash = GetComponentInChildren<SpriteRenderer>().material;
        shake = FindObjectOfType<CameraShake>();
        Debug.Log(gameObject.name);
    }

    void Update()
    {
        if (flashTimer > 0) flash.SetFloat("_FlashAmount", 1);
        else flash.SetFloat("_FlashAmount", 0);

        flashTimer -= Time.deltaTime;
    }




    public void DeathStuff()
    {
        if (deathEffect != null) Instantiate(deathEffect, transform.position, Quaternion.identity);
        if (deathSound != null) audioPlayer.PlayClip(deathSound, deathVolume);
        //shake.setShake(deathShakeAmount, ShakeDecay);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (!player || FindObjectOfType<GameManager>().stagePlaying)
            health -= damage;
        flashTimer = flashLength;
        if (hitSounds.Length > 0)
        {
            AudioClip hitSound = hitSounds[Random.Range(0, hitSounds.Length)];
            if (hitSound != null) audioPlayer.PlayClip(hitSound, hitVolume);
        }
        shake.setShake(hitShakeAmount, ShakeDecay);
        if (health <= 0)
        {
            DeathStuff();
        }
    }

    public int GetHealth()
    {
        return health;
    }



}
