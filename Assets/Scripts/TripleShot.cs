using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _tripleShot;
    

    private void Awake()
    {

    }

    private void Start()
    {
        _tripleShot.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, -1 * _moveSpeed * Time.deltaTime, 0);
        transform.Translate(direction);
    }

    public void OnPlayerPickUp()
    {
        _tripleShot.SetActive(true);
    }
}
