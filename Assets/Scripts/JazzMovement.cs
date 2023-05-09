using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JazzMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed; 
    [SerializeField] float moveDuration; 

    [SerializeField] float moveDelay;
    [SerializeField] float moveVariance;
    [SerializeField] float initialDelay;

    [SerializeField] AudioClip[] moveSounds; 
    [SerializeField] float moveVolume; 

    Rigidbody2D rb;
    Vector2 moveVec; //dont calculate every frame cause that's bad

    AudioPlayer audioplayer;
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        audioplayer = FindObjectOfType<AudioPlayer>();
        moveVec = new Vector2(-moveSpeed, 0);
        StartCoroutine(Movement());
    }

    IEnumerator Movement() {
        yield return new WaitForSeconds(initialDelay);
        while (true) {
            rb.velocity = moveVec;
            AudioClip moveSound = moveSounds[Random.Range(0, moveSounds.Length)];
            if (moveSound != null) audioplayer.PlayClip(moveSound, moveVolume);
            yield return new WaitForSeconds(moveDuration);
            rb.velocity = new Vector2(0, 0);
            /*if (col.IsTouchingLayers(LayerMask.GetMask("Tower"))) { 
                Debug.Log("oshit");
                rb.velocity = -moveVec;
                yield return new WaitForSeconds(moveDuration);
                rb.velocity = new Vector2(0, 0);
            } */
            yield return new WaitForSeconds(moveDelay + Random.Range(0, moveVariance));
        }
    }

}
