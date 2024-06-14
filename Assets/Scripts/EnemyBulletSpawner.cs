using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

public class EnemyBulletSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnCooldown = 0.5f, spawnX, spawnMinY, spawnMaxY;
    [SerializeField]
    private GameObject bulletPrefab;

    private float spawnTimer = 0f;

    static EnemyBulletSpawner instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnCooldown )
        {
            spawnTimer = 0f;
            SpawnBullet();
        }

        // Quitting the game (because player gets destroyed on death)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/playMusic", 0);
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/walking", 0);
            //If we are running in a standalone build of the game
            #if UNITY_STANDALONE
            //Quit the application
            Application.Quit();
            #endif

            //If we are running in the editor
            #if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }


    }

    void SpawnBullet() {
        if( bulletPrefab != null )
        {
            Vector2 spawnPos = new Vector2(spawnX, Random.Range(spawnMinY, spawnMaxY));
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        }
    }
}
