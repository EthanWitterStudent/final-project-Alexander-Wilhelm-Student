using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] bool destroyOnHit;

    public int GetDamage() {
        return damage;
    }

    public void Hit() {
        if (destroyOnHit) Destroy(gameObject);
    }
}
