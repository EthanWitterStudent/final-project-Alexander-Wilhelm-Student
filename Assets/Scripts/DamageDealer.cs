using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] bool destroyOnHit;
    [SerializeField] bool collisionEnterOnly;
    [SerializeField] bool colliderOnly = true;

    [SerializeField] float damageInterval;
    [SerializeField] Collider2D damageCollider;

    [SerializeField] AudioClip[] damageSounds;
    [SerializeField] float damageVolume;
    AudioPlayer audioPlayer;
    float timer;

    void Start() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
    }
    public int GetDamage()
    {
        return damage;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (collisionEnterOnly && (damageCollider == null || other.collider == damageCollider))
        {
            Health health = other.gameObject.GetComponent<Health>();

            if (health != null && timer < 0)
            {
                health.TakeDamage(damage);
                timer = damageInterval;
                if (destroyOnHit) Destroy(gameObject);
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!collisionEnterOnly && (damageCollider == null || other.collider == damageCollider))
        {
            Health health = other.gameObject.GetComponent<Health>();

            if (health != null && timer < 0)
            {
                health.TakeDamage(damage);
                timer = damageInterval;
                if (destroyOnHit) Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!colliderOnly && collisionEnterOnly)
        {
            Health health = other.gameObject.GetComponent<Health>();

            if (health != null && timer < 0)
            {
                health.TakeDamage(damage);
                timer = damageInterval;
                if (destroyOnHit) Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!colliderOnly && !collisionEnterOnly)
        {
            Health health = other.gameObject.GetComponent<Health>();

            if (health != null && timer < 0)
            {
                health.TakeDamage(damage);
                timer = damageInterval;
                if (destroyOnHit) Destroy(gameObject);
            }
        }
    }

    void PlaySound() {
        if (damageSounds.Length > 0) {
            AudioClip damageSound = damageSounds[Random.Range(0, damageSounds.Length)];
            if (damageSound != null) audioPlayer.PlayClip(damageSound, damageVolume);
        }
    }


    public float GetTimer()
    {
        return timer;
    }


}
