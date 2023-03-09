using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
    
{
     public Animator PlayerAnimator;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Vector2 moveDirection;



    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        PlayerAnimator.SetFloat("Horizontal", moveDirection.x);
        PlayerAnimator.SetFloat("Vertical", moveDirection.y);
        Move();
        Inputs();
    }

    private void FixedUpdate()
    {

    }
    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }


}