using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject EnemySpawner;
    
    public float enemySpeed = 2f;

    private void Awake()
    {
        SpawnManager spawnManager = GetComponent<SpawnManager>();
    }

    void Start()
    {
        //transform.position = new Vector3(Random.Range(-9.13f, 10.37f), 5.5f, 0);
    }

    void Update()
    {
        CalculateEnemyMovement();
    }

    void CalculateEnemyMovement()
    {
        //Move enemy down
        Vector3 direction = new Vector3(0, -1 * enemySpeed, 0) * Time.deltaTime;
        transform.Translate(direction);

        //if @ bottom of screen respawns @ top @ a random pos on the X axis
        Vector3 randomX = new Vector3(Random.Range(-9.13f, 10.37f), 10f, 0);
        if (transform.position.y < -5.6f)
        {
            transform.position = randomX;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other == laser destroy the laser then this object
        if(other.CompareTag("Laser"))
        {
            Laser laser = GetComponent<Laser>();

            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        //if other == player damage the player then destroy this object
        if (other.CompareTag("Player"))
        {
            Player player = GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            other.transform.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
    }
}
