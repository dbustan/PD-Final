using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float life = 6f;
    [SerializeField] private float bulletSpeed = 10f;
    void Awake()
    {
        Destroy(gameObject, life);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.CompareTag("PlayerBullet"))
        {
            Destroy(other);
            Destroy(gameObject);
        }
    }
}
