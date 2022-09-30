using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private int _moveSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, -1 * _moveSpeed * Time.deltaTime, 0);
        transform.Translate(direction);
    }
}
