using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _laserSpeed = 25f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 direction = Vector3.up;
        transform.Translate(direction * _laserSpeed * Time.deltaTime);

        if(transform.position.y > 9f)
        {
            Destroy(this.gameObject);
        }
    }
}
