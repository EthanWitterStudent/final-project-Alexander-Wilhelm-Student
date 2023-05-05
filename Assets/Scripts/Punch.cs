using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] Collider2D punchCollider;
    [SerializeField] int punchDamage;
    [SerializeField] float punchDuration;

    [SerializeField] float punchKnockback;

    [SerializeField] GameObject punchEffect;
    [SerializeField] Vector3 offset;
    [SerializeField] string animatorTrigger;
    [SerializeField] AudioClip punchSound;
    [SerializeField] float punchVolume;
    float timer;
    Animator animator;

    AudioPlayer audioplayer;

    Vector2 kbVector;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioplayer = FindObjectOfType<AudioPlayer>();
        kbVector = new Vector2(punchKnockback, 0);
        
    }

    private void FixedUpdate() {
        timer -= Time.fixedDeltaTime;
    }

    private void OnTriggerStay2D(Collider2D other) {  //this would probably be better as a coroutine.   too bad!
        if (timer <= 0 && other.gameObject.tag == "Enemy") {
            if (animator != null && animatorTrigger != null) animator.SetTrigger(animatorTrigger);

            //HOW TO PUNCH ALL ENEMYIES AND MAK THEM DIED
            //first we're gonna get all the enemies we can currently touch
            ContactFilter2D filter = new ContactFilter2D(); filter.SetLayerMask(LayerMask.GetMask("Enemy"));  //get a filter going
            List<Collider2D> results = new List<Collider2D>();  //prepare a list where we'll store the colliders of enemies we can punch

            if (punchCollider.OverlapCollider(filter, results) > 0) {       //checks AGAIN to make sure we're actually touching enemies, and stores the results of the check
            foreach (Collider2D col in results) {                           //for all the collider2ds we hit...
                if (col.isTrigger) continue;                                //only hit collision colliders, which we should only have one of anyway
                GameObject gobj = col.gameObject;                           //get the actual enemy
                if (Mathf.Abs(punchKnockback) > Mathf.Epsilon) other.GetComponent<Rigidbody2D>().AddForce(kbVector, ForceMode2D.Impulse); //knockback
                other.GetComponent<Health>().TakeDamage(punchDamage); //damage
                // this only works properly if the enemy has only one proper collider2d. 
            }
            // there is probably a more efficient way to do this.                                           too bad!
            
        }
            
            audioplayer.PlayClip(punchSound, punchVolume);
            if (punchEffect != null) Instantiate(punchEffect, transform.position + offset, Quaternion.identity);
            timer = punchDuration;
        }
    }
}
