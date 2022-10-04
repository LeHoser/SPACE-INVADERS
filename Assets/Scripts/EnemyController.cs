using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemy;

    [SerializeField] public float enemySpeed;
    [SerializeField] private TripleShot _trishot;
    [SerializeField] private HealthPickUp _healthPickUp;
    [SerializeField] private bool _spawnTriShot;
    [SerializeField] private bool _spawnHealthPickUp;
    [SerializeField] private bool _trishotPickedUp = false;


    private void Awake()
    {
        SpawnManager spawnManager = GetComponent<SpawnManager>();
        Player player = GetComponent<Player>();
    }

    void Start()
    {
        _trishotPickedUp = false;
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

            int healthSpawnNumber = 15;
            int healthSpawnChance = Random.Range(1, 16);

            if(spawnNumber > randomChance)
            {
                _spawnTriShot = false;
            }

            if(spawnNumber == randomChance)
            {
                if (_trishotPickedUp == false)
                {
                    _spawnTriShot = true;
                }
                else if(_trishotPickedUp == true)
                {
                    _spawnTriShot = false;
                }
            }

            if(_spawnTriShot == true && _trishotPickedUp == true)
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }

            else if (_spawnTriShot == true && _trishotPickedUp == false)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                Instantiate(_trishot, spawnPosition, Quaternion.identity);
                _spawnTriShot = false;
            }

            else if(_spawnTriShot == false)
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }

            if(healthSpawnNumber == healthSpawnChance)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                Instantiate(_healthPickUp, spawnPosition, Quaternion.identity);
            }
            else if(healthSpawnNumber > healthSpawnChance)
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

    public void TrishotAcquired()
    {
        _trishotPickedUp = true;
    }
}
