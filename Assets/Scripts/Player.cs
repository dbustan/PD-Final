using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    Vector2 movement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed *Time.fixedDeltaTime);
        spriteRenderer.flipX = movement.x < 0f;

        if (Input.GetKeyDown(KeyCode.X)){
            ShootWeapon();
        }
    }

    private void ShootWeapon(){

    }
}
