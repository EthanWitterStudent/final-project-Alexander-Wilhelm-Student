using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] Vector2 knockback;
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

    public void SetDamage(int value)
    {
        damage = value;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (collisionEnterOnly && (damageCollider == null || other.collider == damageCollider))
        {
            DamageCheck(other);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!collisionEnterOnly && (damageCollider == null || other.collider == damageCollider))
        {
            DamageCheck(other);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!colliderOnly && collisionEnterOnly)
        {
            DamageCheck(other);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!colliderOnly && !collisionEnterOnly)
        {
            DamageCheck(other);
        }
    }

    void DamageCheck(Collider2D other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (multipleDamage) MultipleDamage(); else SimpleDamage(health);
        if (health != null) DamageStuff(true); else DamageStuff(false);
    }

    void DamageCheck(Collision2D other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (multipleDamage) MultipleDamage(); else SimpleDamage(health);
        if (health != null) DamageStuff(true); else DamageStuff(false);
    }


    void PlaySound()
    {
        if (damageSounds.Length > 0)
        {
            AudioClip damageSound = damageSounds[Random.Range(0, damageSounds.Length)];
            if (damageSound != null) audioPlayer.PlayClip(damageSound, damageVolume);
        }
    }

    void DamageStuff(bool sound)
    {
        if (damageEffect != null) Instantiate(damageEffect, transform.position, Quaternion.identity);


        if (sound) PlaySound();
        if (destroyOnHit) Destroy(gameObject);

    }

    void SimpleDamage(Health health)
    {
        if (health != null && timer < 0)
        {
            health.TakeDamage(damage);
            health.gameObject.GetComponent<Rigidbody2D>().AddForce(knockback);
            timer = damageInterval;
        }
    }


    public float GetTimer()
    {
        return timer;
    }

    void MultipleDamage()
    {
        if (timer <= Mathf.Epsilon)
        {
            ContactFilter2D filter = new ContactFilter2D();   //for some reason explosions seem to hurt both teams... hopefully this fixes it
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            int towerLayer = LayerMask.NameToLayer("Tower");

            if (gameObject.layer == enemyLayer) filter.SetLayerMask(LayerMask.GetMask("Tower"));
            else if (gameObject.layer == towerLayer) filter.SetLayerMask(LayerMask.GetMask("Enemy"));
            else
            {
                Debug.LogWarning($"{gameObject.name}'s Multiple Damage Dealer not assigned to enemy or tower layer! Fix!!!");
                filter.SetLayerMask(LayerMask.GetMask("Enemy", "Tower"));
            }

            List<Collider2D> results = new List<Collider2D>();

            if (damageCollider.OverlapCollider(filter, results) > 0)
            {
                foreach (Collider2D col in results)
                {
                    if (col.isTrigger) continue;
                    GameObject gobj = col.gameObject;
                    col.GetComponent<Health>().TakeDamage(damage); //damage
                    col.gameObject.GetComponent<Rigidbody2D>().AddForce(knockback);
                    // this only works properly if the enemy has only one proper collider2d. 
                }
            }
            timer = damageInterval;
        }
    }
}
