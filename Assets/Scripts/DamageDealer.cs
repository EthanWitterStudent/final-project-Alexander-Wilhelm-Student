using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] GameObject damageEffect;
    [SerializeField] bool multipleDamage;
    [SerializeField] bool destroyOnHit;
    [SerializeField] bool collisionEnterOnly;
    [SerializeField] bool colliderOnly = true;

    [SerializeField] float damageInterval;
    [SerializeField] Collider2D damageCollider;

    [SerializeField] AudioClip[] damageSounds;
    [SerializeField] float damageVolume;
    AudioPlayer audioPlayer;
    float timer;

    void Start()
    {
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
            if (multipleDamage) MultipleDamage(); else SimpleDamage(health);
            DamageStuff();
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!collisionEnterOnly && (damageCollider == null || other.collider == damageCollider))
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (multipleDamage) MultipleDamage(); else SimpleDamage(health);
            DamageStuff();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!colliderOnly && collisionEnterOnly)
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (multipleDamage) MultipleDamage(); else SimpleDamage(health);
            DamageStuff();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!colliderOnly && !collisionEnterOnly)
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (multipleDamage) MultipleDamage(); else SimpleDamage(health);
            DamageStuff();
        }
    }

    void PlaySound()
    {
        if (damageSounds.Length > 0)
        {
            AudioClip damageSound = damageSounds[Random.Range(0, damageSounds.Length)];
            if (damageSound != null) audioPlayer.PlayClip(damageSound, damageVolume);
        }
    }

    void DamageStuff()
    {
            if (damageEffect != null) Instantiate(damageEffect, transform.position, Quaternion.identity);


            PlaySound();
            if (destroyOnHit) Destroy(gameObject);
        
    }

    void SimpleDamage(Health health)
    {
        if (health != null && timer < 0)
        {
            health.TakeDamage(damage);
            timer = damageInterval;
        }
    }


    public float GetTimer()
    {
        return timer;
    }

    void MultipleDamage()
    {
        if (timer < 0)
        {
            ContactFilter2D filter = new ContactFilter2D(); filter.SetLayerMask(LayerMask.GetMask("Enemy"));
            List<Collider2D> results = new List<Collider2D>();

            if (damageCollider.OverlapCollider(filter, results) > 0)
            {
                foreach (Collider2D col in results)
                {
                    if (col.isTrigger) continue;
                    GameObject gobj = col.gameObject;
                    col.GetComponent<Health>().TakeDamage(damage); //damage
                                                                   // this only works properly if the enemy has only one proper collider2d. 
                }
            }
            timer = damageInterval;
        }
    }
}
