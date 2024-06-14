using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityOSC;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    private float speed = 0f;
    private bool isMoving = false;  // For playing footstep noise

    public SpriteRenderer playerSpriteRenderer;
    public Animator animator;

    public Transform bulletSpawnPos;

    public GameObject bulletPrefab;

    public float bulletSpeed = 10f;

    [SerializeField] int health = 3;



    Vector2 movement;
    void Start()
    {
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/playMusic", 1);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        speed = movement.sqrMagnitude;
        animator.SetFloat("Speed", speed);

        if (!isMoving && speed > 0)
        {
            isMoving = true;
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/walking", 1);
        } else if (isMoving && speed < 0.1)
        {
            isMoving = false;
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/walking", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            ShootBullet();
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/gunshot", 1);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Enemy"))
        {
            Destroy(other);
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/deathSound", 1);

            health--;
            if(health <= 0)
            {
                OSCHandler.Instance.SendMessageToClient("pd", "/unity/walking", 0);
                Destroy(gameObject);
            }
        }
        
    }

}
