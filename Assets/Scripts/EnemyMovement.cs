using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed; 
    Rigidbody2D rb;
    Vector2 moveVec; //dont calculate every frame cause that's bad
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVec = new Vector2(-moveSpeed, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = moveVec * Time.fixedDeltaTime;
    }

    public void SetEnemySpeed(float x) {
        moveVec = new Vector2(-x, 0);
    }
}
