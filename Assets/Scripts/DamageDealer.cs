using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] bool destroyOnHit;
    [SerializeField] bool collisionEnterOnly;

    [SerializeField] float damageInterval;
    [SerializeField] Collider2D damageCollider;
    float timer;

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
        if (collisionEnterOnly)
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
        if (!collisionEnterOnly)
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


    public float GetTimer()
    {
        return timer;
    }


}
