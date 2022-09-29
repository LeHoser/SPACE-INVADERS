using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemy;

    [SerializeField] private TripleShot _trishot;
    public float enemySpeed;
    private bool _spawnTriShot;

    private void Awake()
    {
        SpawnManager spawnManager = GetComponent<SpawnManager>();
    }

    void Start()
    {
        _spawnTriShot = false;
    }

    void Update()
    {
        CalculateEnemyMovement();
    }

    void CalculateEnemyMovement()
    {
        Vector3 direction = new Vector3(0, -1 * enemySpeed, 0) * Time.deltaTime;
        transform.Translate(direction);

        Vector3 randomX = new Vector3(Random.Range(-9.13f, 10.37f), 10f, 0);
        if (transform.position.y < -5.6f)
        {
            transform.position = randomX;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Laser"))
        {
            Laser laser = GetComponent<Laser>();

            int spawnNumber = 10;
            int randomChance = Random.Range(1, 11);
            print(randomChance);

            if(spawnNumber <= randomChance)
            {
                _spawnTriShot = true;
            }

            if(_spawnTriShot == true)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                Instantiate(_trishot, spawnPosition, Quaternion.identity);
            }

            else if(_spawnTriShot == false)
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }

        }

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
