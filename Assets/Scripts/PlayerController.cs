using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody _playerRb;
    private GameObject _focalPoint;
    public bool hasPowerUp = false;
    public float powerBounce = 10;
    public GameObject indicatorPowerUp;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float _inputForward = Input.GetAxis("Vertical");
        _playerRb.AddForce(_focalPoint.transform.forward * _inputForward * speed);
        indicatorPowerUp.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    IEnumerator GetRespawnPowerUp()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        indicatorPowerUp.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            indicatorPowerUp.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine("GetRespawnPowerUp");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 enemyBounce = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(enemyBounce * powerBounce, ForceMode.Impulse);
            Debug.Log("You collide with " + collision.gameObject.name + "You has power Up" + hasPowerUp);
        }
    }
}