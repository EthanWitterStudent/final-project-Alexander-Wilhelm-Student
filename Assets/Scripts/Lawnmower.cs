using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lawnmower : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float destroyPastX;
    [SerializeField] AudioClip activateSound;
    [SerializeField] float soundVolume;
    [SerializeField] float screenShake;
    [SerializeField] float shakeDecay;
    bool mowing;

    Rigidbody2D rb;
    AudioPlayer audioplayer;

    Vector2 mowSpeed; //calculate once to reduce overhead

    void Start() {
        audioplayer = FindObjectOfType<AudioPlayer>();
        rb =  GetComponent<Rigidbody2D>();
        mowSpeed = new Vector2(moveSpeed, 0);
    }

    private void FixedUpdate() {
        if (mowing) {
            rb.velocity = mowSpeed * Time.fixedDeltaTime;
            if (rb.position.x > destroyPastX) Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Health>().DeathStuff();   //kill em
            if (activateSound != null &! mowing) audioplayer.PlayClip(activateSound, soundVolume); //loud = funny
            mowing = true; //only start mowing once we kill a guy
            FindObjectOfType<CameraShake>().setShake(screenShake, shakeDecay);
        }
    }
}
