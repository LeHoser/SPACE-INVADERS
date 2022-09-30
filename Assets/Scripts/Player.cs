using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 6.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.35f;
    [SerializeField] private float _canFire = -1f;
    [SerializeField] private int _playerHealth;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private bool _trishotUpgrade;

    private void Awake()
    {

    }

    void Start()
    {
        _trishotUpgrade = false;
        _playerHealth = 3;
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();

        if(Input.GetKeyDown(KeyCode.Space) && _trishotUpgrade == false &&  Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Vector3 offSet = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            Instantiate(_laserPrefab, offSet, Quaternion.identity);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && _trishotUpgrade == true && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Vector3 offSetLeft = new Vector3(transform.position.x - 0.2f, transform.position.y + 1, 0);
            Vector3 offSetCenter = new Vector3(transform.position.x, transform.position.y + 1, 0);
            Vector3 offSetRight = new Vector3(transform.position.x + 0.2f, transform.position.y + 1, 0);
            Instantiate(_laserPrefab, offSetLeft, Quaternion.identity);
            Instantiate(_laserPrefab, offSetCenter, Quaternion.identity);
            Instantiate(_laserPrefab, offSetRight, Quaternion.identity);
        }
    }

    void CalculateMovement()
    {
        //setting up movement controls as local variables
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //calculating movement and player speed
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _playerSpeed * Time.deltaTime);

        //keeps the player within bounds on the Y axis
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0f), 0);

        //if the player goes off the screen left or right, the players appears on the opposite side
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        _playerHealth -= 1;

        if(_playerHealth < 1)
        {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trishot"))
        {
            Destroy(other.gameObject);
            _spawnManager.playerHasTriShot = true;
            _trishotUpgrade = true;
            //_trishot.OnPlayerPickUp();
            print("Player has picked up the upgrade");
        }
        if(other.CompareTag("HealthPickUp"))
        {
            _playerHealth += 1;
            Destroy(other.gameObject);
        }
    }
}
