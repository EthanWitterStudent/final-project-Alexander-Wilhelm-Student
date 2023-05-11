using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanic : MonoBehaviour


{

    [SerializeField] float moveSpeed;
    [SerializeField] float spawnX;
    [SerializeField] float destroyPastX;
    Rigidbody2D rb;
    AudioPlayer audioplayer;
    DamageDealer damage;
    Vector2 moveVec;
    void Start() {
        audioplayer = FindObjectOfType<AudioPlayer>();
        rb =  GetComponent<Rigidbody2D>();
        damage = GetComponent<DamageDealer>();
        damage.enabled = true;
        Invoke("enableSprite", 0.05f);
        rb.position = new Vector2(spawnX, rb.position.y);
        moveVec = new Vector2(moveSpeed, 0);
    }
    

    private void FixedUpdate() {
        rb.velocity = moveVec * Time.fixedDeltaTime;
        if (rb.position.x > destroyPastX) Destroy(gameObject);
    }

    private void enableSprite() {
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
}
