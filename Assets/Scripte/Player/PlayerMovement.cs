using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _moveH;
    private float _moveV;
    public float speed;
    public GameObject gunPrefabs;
    private bool _amIShooting;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Shoot();
    }

    private void Movement()
    {
        _moveH = Input.GetAxis("Horizontal");
        _moveV = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.A)&&!_amIShooting)
        {
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            _rigidbody2D.rotation = -90;
        }
        else if (Input.GetKey(KeyCode.D)&&!_amIShooting)
        {
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            _rigidbody2D.rotation = 90;
        }
        else if (Input.GetKey(KeyCode.W)&&!_amIShooting)
        {
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            _rigidbody2D.rotation = 180;
        }
        else if (Input.GetKey(KeyCode.S)&&!_amIShooting)
        {
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            _rigidbody2D.rotation = 0;
        }

        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _moveV * speed);
        _rigidbody2D.velocity = new Vector2(_moveH * speed, _rigidbody2D.velocity.y);
    }

    private void Shoot()
    {
      
        if (Input.GetMouseButtonDown(0))
        {
            var clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.LookAt(clickedPosition,Vector3.back);
            _amIShooting = true;
            var bullet = Instantiate(gunPrefabs, transform.position, gunPrefabs.transform.rotation);
            bullet.GetComponent<Bullet>().SetDirection(-transform.up);
            
        }
    }
}