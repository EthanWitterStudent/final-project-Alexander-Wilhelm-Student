using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] bool destroyOnHit;

    [SerializeField] float damageInterval;
    float timer;

    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
    }
    public int GetDamage()
    {
        return damage;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Health health = other.gameObject.GetComponent<Health>();

        if (health != null && timer < 0)
        {
            health.TakeDamage(damage);
            timer = damageInterval;
            if (destroyOnHit) Destroy(gameObject);
        }

    }

    public float GetTimer()
    {
        return timer;
    }


}
