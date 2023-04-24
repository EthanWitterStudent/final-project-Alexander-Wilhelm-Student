using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector2 moveInput;

    Vector2 minBounds;
    Vector2 maxBounds;
    [SerializeField] float padLeft;
    [SerializeField] float padRight;
    [SerializeField] float padTop;
    [SerializeField] float padBottom;
    
    PlayerShoot shooter;
    float dt;

    void Start(){
        InitBounds();
        shooter = GetComponent<PlayerShoot>();
    }
    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        Move();
    }

    void InitBounds()
    {
        Camera mainCam = Camera.main;
        minBounds = mainCam.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCam.ViewportToWorldPoint(new Vector2(1,1));
    }
    void Move()
    {

        Vector2 delta = moveInput * dt * moveSpeed;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + padLeft, maxBounds.x - padRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + padBottom, maxBounds.y - padTop);
        transform.position = newPos;
        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }

    void OnFire(InputValue value) {
        if (shooter != null) shooter.shooting = value.isPressed;

        Debug.Log(value.isPressed);
    }
}
