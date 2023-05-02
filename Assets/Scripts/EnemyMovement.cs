using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float maxSpeed; 
    [SerializeField] float moveAccel; 
    Rigidbody2D rb;
    Vector2 moveVec; //dont calculate every frame cause that's bad
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVec = new Vector2(-moveAccel, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(moveVec);
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        rb.velocity = new Vector2 (Mathf.Clamp(rb.velocity.x, -maxSpeed, Mathf.Infinity), rb.velocity.y);
    }

    public void SetEnemyAccel(float x) {
        moveVec = new Vector2(-x, 0);
    }

    public void SetEnemyMaxSpeed(float x) {
        maxSpeed = x;
    }
}
