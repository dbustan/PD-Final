using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public SpriteRenderer playerSpriteRenderer;
    public Animator animator;

    public Transform bulletSpawnPos;

    public GameObject bulletPrefab;

    public float bulletSpeed = 50000;



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
        if (Input.GetKeyDown(KeyCode.X)){
            ShootBullet();
            
        }
        
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed *Time.fixedDeltaTime);
        playerSpriteRenderer.flipX = movement.x < 0f;

        
    }
    private void ShootBullet(){
        var bullet =  Instantiate(bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
        SpriteRenderer bulletSpriteRenderer =  bullet.GetComponent<SpriteRenderer>();
        bulletSpriteRenderer.flipX = playerSpriteRenderer.flipX;
        if(bulletSpriteRenderer.flipX){
            Debug.Log("getting to left");
            bullet.GetComponent<Rigidbody2D>().velocity = -bulletSpawnPos.right * bulletSpeed;
            
        } else {
            Debug.Log("getting to right");
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPos.right * bulletSpeed;
            
        }
        
    }

}
